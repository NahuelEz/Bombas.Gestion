using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Clover.DbLayer;
using MySql.Data.MySqlClient;
using System.Drawing;



namespace Clover.Gestion
{
    public partial class CRM_MAIN : Form
    {
        private int userId; // Usuario autenticado
        private int userAccessLevel; // Nivel de acceso del usuario autenticado
        public CRM_MAIN()
        {
            InitializeComponent();
            userId = AppEnvironment.CurrentUser.UserID;
            userAccessLevel = AppEnvironment.CurrentUser.AccessLevel;

            this.Load += CRM_MAIN_Load;
        }

        private void CRM_MAIN_Load(object sender, EventArgs e)
        {
            CargarLeadsPorEstado(flowPanelLeadsNuevos, "Nuevo");
            CargarLeadsPorEstado(flowPanelCalificacion, "Calificación");
            CargarLeadsPorEstado(flowPanelAsesoria, "Asesoría");
            CargarLeadsPorEstado(flowPanelNegociacion, "Negociación");
            CargarLeadsPorEstado(flowPanelCierre, "Cierre");

            ConfigurarMenusContextuales();
            ConfigurarEventosDragDrop();
        }

        private LeadCard leadSeleccionado;



        private void LeadCard_Click(object sender, EventArgs e)
        {
            if (sender is LeadCard leadCard)
            {
                leadSeleccionado = leadCard;
                MessageBox.Show($"Lead seleccionado: {leadCard.LeadName}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ToolTip toolTipCRM = new ToolTip();

        private void CargarLeadsPorEstado(FlowLayoutPanel flowPanel, string estado)
        {
            DataTable leads = new DataTable();

            // Modificar la consulta según el nivel de acceso
            string query = userAccessLevel == 5
                ? "SELECT * FROM Leads WHERE Estado = @Estado" // Administrador ve todos los leads
                : "SELECT * FROM Leads WHERE Estado = @Estado AND UsuarioID = @UsuarioID"; // Usuario solo ve sus leads

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        if (userAccessLevel != 5)
                        {
                            cmd.Parameters.AddWithValue("@UsuarioID", userId);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(leads);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los leads: " + ex.Message);
                return;
            }

            flowPanel.Controls.Clear();

            foreach (DataRow row in leads.Rows)
            {
                LeadCard card = new LeadCard
                {
                    LeadID = Convert.ToInt32(row["LeadID"]),
                    LeadName = row["Nombre"].ToString(),
                    LeadDate = DateTime.Parse(row["FechaCreacion"].ToString()).ToString("dd/MM/yyyy")
                };

                string nivelUrgencia = row["NivelUrgencia"].ToString();
                card.AplicarColorPorNivelUrgencia(nivelUrgencia);

                toolTipCRM.SetToolTip(card, $"Lead: {card.LeadName}\nFecha: {card.LeadDate}\nUrgencia: {nivelUrgencia}");

                flowPanel.Controls.Add(card);
            }
        }




        private void ConfigurarEventosDragDrop()
        {
            ConfigurarFlowPanelParaDragDrop(flowPanelLeadsNuevos);
            ConfigurarFlowPanelParaDragDrop(flowPanelCalificacion);
            ConfigurarFlowPanelParaDragDrop(flowPanelAsesoria);
            ConfigurarFlowPanelParaDragDrop(flowPanelNegociacion);
            ConfigurarFlowPanelParaDragDrop(flowPanelCierre);
        }

        private void ConfigurarFlowPanelParaDragDrop(FlowLayoutPanel panel)
        {
            panel.AllowDrop = true;
            panel.DragEnter += flowPanel_DragEnter;
            panel.DragDrop += flowPanel_DragDrop;
        }

        private void CargarLeadsNuevos(string nombre = null, string urgencia = null, DateTime? fecha = null)
        {
            DataTable leadsFiltrados = new DataTable();


            string query = "SELECT * FROM Leads WHERE Estado = 'Nuevo'";


            List<string> condiciones = new List<string>();


            if (!string.IsNullOrEmpty(nombre))
                condiciones.Add("Nombre LIKE @Nombre");
            if (!string.IsNullOrEmpty(urgencia))
                condiciones.Add("NivelUrgencia = @Urgencia");
            if (fecha.HasValue)
                condiciones.Add("DATE(FechaCreacion) = @Fecha");


            if (condiciones.Count > 0)
                query += " AND " + string.Join(" AND ", condiciones);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {

                        if (!string.IsNullOrEmpty(nombre))
                            cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                        if (!string.IsNullOrEmpty(urgencia))
                            cmd.Parameters.AddWithValue("@Urgencia", urgencia);
                        if (fecha.HasValue)
                            cmd.Parameters.AddWithValue("@Fecha", fecha.Value.Date);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(leadsFiltrados);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los leads: " + ex.Message);
                return;
            }


            flowPanelLeadsNuevos.Controls.Clear();


            foreach (DataRow row in leadsFiltrados.Rows)
            {
                LeadCard card = new LeadCard
                {
                    LeadID = Convert.ToInt32(row["LeadID"]),
                    LeadName = row["Nombre"].ToString(),
                    LeadDate = DateTime.Parse(row["FechaCreacion"].ToString()).ToString("dd/MM/yyyy")
                };

                string nivelUrgencia = row["NivelUrgencia"].ToString();
                card.AplicarColorPorNivelUrgencia(nivelUrgencia);

                flowPanelLeadsNuevos.Controls.Add(card);
            }
        }




        private void flowPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(LeadCard)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void flowPanel_DragDrop(object sender, DragEventArgs e)
        {
            LeadCard card = (LeadCard)e.Data.GetData(typeof(LeadCard));
            FlowLayoutPanel panel = (FlowLayoutPanel)sender;
            panel.Controls.Add(card);


            string nuevoEstado = ObtenerEstadoPorPanel(panel.Name);
            ActualizarEstadoLeadEnBaseDatos(card.LeadID, nuevoEstado);


            card.RecargarDatos();
        }





        private string ObtenerEstadoPorPanel(string panelName)
        {
            switch (panelName)
            {
                case "flowPanelLeadsNuevos":
                    return "Nuevo";
                case "flowPanelCalificacion":
                    return "Calificación";
                case "flowPanelAsesoria":
                    return "Asesoría";
                case "flowPanelNegociacion":
                    return "Negociación";
                case "flowPanelCierre":
                    return "Cierre";
                default:
                    return "Nuevo";
            }

        }


        private void ActualizarEstadoLeadEnBaseDatos(int leadId, string nuevoEstado)
        {
            string query = "UPDATE Leads SET Estado = @Estado WHERE LeadID = @LeadID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        private void btnAgregarLead_Click(object sender, EventArgs e)
        {
            LeadForm leadForm = new LeadForm();
            if (leadForm.ShowDialog() == DialogResult.OK)
            {
                CargarLeadsNuevos();
            }
        }

        private void btnFiltrosAvanzados_Click(object sender, EventArgs e)
        {
            FiltrosAvanzadosForm filtrosForm = new FiltrosAvanzadosForm();
            if (filtrosForm.ShowDialog() == DialogResult.OK)
            {
                CargarLeadsNuevos(
                    filtrosForm.FiltroNombre,
                    filtrosForm.FiltroUrgencia,
                    filtrosForm.FiltroFecha
                );
            }
        }

        private void btnResetFiltros_Click(object sender, EventArgs e)
        {

            CargarLeadsNuevos();


            txtBuscarLeads.Text = "";
        }


        private void ConfigurarMenusContextuales()
        {
            ConfigurarMenuContextualParaCasilla(flowPanelLeadsNuevos);
            ConfigurarMenuContextualParaCasilla(flowPanelCalificacion);
            ConfigurarMenuContextualParaCasilla(flowPanelAsesoria);
            ConfigurarMenuContextualParaCasilla(flowPanelNegociacion);
            ConfigurarMenuContextualParaCasilla(flowPanelCierre);
        }

        private void ConfigurarMenuContextualParaCasilla(FlowLayoutPanel flowPanel)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Opciones del menú
            ToolStripMenuItem ordenarPorFecha = new ToolStripMenuItem("Ordenar por Fecha");
            ordenarPorFecha.Click += (sender, e) => OrdenarCards(flowPanel, "Ordenar por Fecha");
            contextMenu.Items.Add(ordenarPorFecha);

            ToolStripMenuItem ordenarPorUrgencia = new ToolStripMenuItem("Ordenar por Urgencia");
            ordenarPorUrgencia.Click += (sender, e) => OrdenarCards(flowPanel, "Ordenar por Urgencia");
            contextMenu.Items.Add(ordenarPorUrgencia);

            // Asignar el menú al FlowLayoutPanel
            flowPanel.ContextMenuStrip = contextMenu;
        }


        private void OrdenarCards(FlowLayoutPanel panel, string criterio)
        {
            // Extraer todas las tarjetas del FlowLayoutPanel
            var leadCards = panel.Controls.OfType<LeadCard>().ToList();

            // Ordenar según el criterio seleccionado
            if (criterio == "Fecha")
            {
                // Ordenar por fecha
                leadCards = leadCards
                    .OrderBy(card =>
                    {
                        // Intentar analizar LeadDate como DateTime
                        if (DateTime.TryParse(card.LeadDate.Replace("Fecha: ", ""), out var fecha))
                            return fecha;
                        return DateTime.MinValue; // Fecha predeterminada para valores no válidos
                    })
                    .ToList();
            }
            else if (criterio == "Urgencia")
            {
                // Ordenar por nivel de urgencia
                leadCards = leadCards
                    .OrderBy(card =>
                    {
                        // Asignar prioridades a los niveles de urgencia
                        return card.BackColor == Color.Red ? 1 :  // "Alto"
                               card.BackColor == Color.Orange ? 2 : // "Medio"
                               card.BackColor == Color.LightGreen ? 3 : 4; // "Bajo" o indefinido
                    })
                    .ToList();
            }

            // Refrescar las tarjetas en el panel
            panel.Controls.Clear(); // Limpiar todas las tarjetas actuales del FlowLayoutPanel
            panel.Controls.AddRange(leadCards.ToArray()); // Agregar las tarjetas ordenadas
        }


        private void ContextMenu_OrdenarPorFecha_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.Owner is ContextMenuStrip menu && menu.SourceControl is FlowLayoutPanel flowPanel)
            {
                OrdenarCards(flowPanel, "Fecha");
            }
        }

        private void ContextMenu_OrdenarPorUrgencia_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem && menuItem.Owner is ContextMenuStrip menu && menu.SourceControl is FlowLayoutPanel flowPanel)
            {
                OrdenarCards(flowPanel, "Urgencia");
            }
        }


        private void txtBuscarLeads_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscarLeads.Text.ToLower();

            foreach (Control control in flowPanelLeadsNuevos.Controls)
            {
                if (control is LeadCard leadCard)
                {
                    leadCard.Visible = leadCard.LeadName.ToLower().Contains(textoBusqueda);
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnInformes_Click(object sender, EventArgs e)
        {
            using (InformesForm informesForm = new InformesForm())
            {
                informesForm.ShowDialog();
            }
        }


        private void btnLeadsCalificacion_Click(object sender, EventArgs e)
        {
            using (LeadsCalificacionForm form = new LeadsCalificacionForm())
            {
                form.ShowDialog();
            }
        }


        private void btnCierreLead_Click(object sender, EventArgs e)
        {

            if (leadSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un lead para cerrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            using (CierreForm cierreForm = new CierreForm(leadSeleccionado.LeadID))
            {
                if (cierreForm.ShowDialog() == DialogResult.OK)
                {

                    CargarLeadsPorEstado(flowPanelCierre, "Cierre"); // Actualizar la casilla de cierre
                    MessageBox.Show("El cierre del lead se realizó correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void AbrirFiltroAvanzado(FlowLayoutPanel panel, string estado)
        {
            using (FiltrosAvanzadosForm filtroForm = new FiltrosAvanzadosForm())
            {
                if (filtroForm.ShowDialog() == DialogResult.OK)
                {
                    // Obtener los filtros aplicados desde el formulario
                    string filtroNombre = filtroForm.FiltroNombre;
                    string filtroUrgencia = filtroForm.FiltroUrgencia;
                    DateTime? filtroFecha = filtroForm.FiltroFecha;

                    // Cargar los leads filtrados en el panel correspondiente
                    CargarLeadsFiltrados(panel, estado, filtroNombre, filtroUrgencia, filtroFecha);
                }
            }
        }

        private void CargarLeadsFiltrados(FlowLayoutPanel panel, string estado, string nombre = null, string urgencia = null, DateTime? fecha = null)
        {
            DataTable leadsFiltrados = new DataTable();

            // Construir la consulta SQL con los filtros
            string query = "SELECT * FROM Leads WHERE Estado = @Estado";
            List<string> condiciones = new List<string>();

            if (!string.IsNullOrEmpty(nombre))
                condiciones.Add("Nombre LIKE @Nombre");
            if (!string.IsNullOrEmpty(urgencia))
                condiciones.Add("NivelUrgencia = @Urgencia");
            if (fecha.HasValue)
                condiciones.Add("DATE(FechaCreacion) = @Fecha");

            if (condiciones.Count > 0)
                query += " AND " + string.Join(" AND ", condiciones);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Estado", estado);
                        if (!string.IsNullOrEmpty(nombre))
                            cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                        if (!string.IsNullOrEmpty(urgencia))
                            cmd.Parameters.AddWithValue("@Urgencia", urgencia);
                        if (fecha.HasValue)
                            cmd.Parameters.AddWithValue("@Fecha", fecha.Value.Date);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(leadsFiltrados);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los leads filtrados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Limpiar el panel y agregar los leads filtrados
            panel.Controls.Clear();

            foreach (DataRow row in leadsFiltrados.Rows)
            {
                LeadCard card = new LeadCard
                {
                    LeadID = Convert.ToInt32(row["LeadID"]),
                    LeadName = row["Nombre"].ToString(),
                    LeadDate = DateTime.Parse(row["FechaCreacion"].ToString()).ToString("dd/MM/yyyy")
                };

                string nivelUrgencia = row["NivelUrgencia"].ToString();
                card.AplicarColorPorNivelUrgencia(nivelUrgencia);

                panel.Controls.Add(card);
            }
        }

        private void FiltrarLeads(FlowLayoutPanel panel, string textoBusqueda)
        {
            textoBusqueda = textoBusqueda.ToLower();

            foreach (Control control in panel.Controls)
            {
                if (control is LeadCard leadCard)
                {
                    leadCard.Visible = leadCard.LeadName.ToLower().Contains(textoBusqueda);
                }
            }
        }


        private void ResetearFiltros(FlowLayoutPanel panel, string estado)
        {
            // Limpiar y cargar todos los leads del estado correspondiente
            CargarLeadsPorEstado(panel, estado);

            // Opcional: Limpiar cualquier texto en cuadros de búsqueda u otros filtros visuales
            txtBuscarLeads.Text = "";
        }


        private void txtBuscarCalificacion_TextChanged(object sender, EventArgs e)
        {
            FiltrarLeads(flowPanelCalificacion, txtBuscarCalificacion.Text);
        }

        private void txtBuscarAsesoria_TextChanged(object sender, EventArgs e)
        {
            FiltrarLeads(flowPanelAsesoria, txtBuscarAsesoria.Text);
        }

        private void txtBuscarNegociacion_TextChanged(object sender, EventArgs e)
        {
            FiltrarLeads(flowPanelNegociacion, txtBuscarNegociacion.Text);
        }

        private void txtBuscarCierre_TextChanged(object sender, EventArgs e)
        {
            FiltrarLeads(flowPanelCierre, txtBuscarCierre.Text);
        }

        private void btnLeadsCerrados_Click(object sender, EventArgs e)
        {
            using (LeadsCerradosForm form = new LeadsCerradosForm())
            {
                form.ShowDialog();
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btnFiltroCalificacion_Click(object sender, EventArgs e)
        {
            AbrirFiltroAvanzado(flowPanelCalificacion, "Calificación");
        }

        private void btnFiltroAsesoria_Click(object sender, EventArgs e)
        {
            AbrirFiltroAvanzado(flowPanelAsesoria, "Asesoría");
        }

        private void btnFiltroNegociacion_Click(object sender, EventArgs e)
        {
            AbrirFiltroAvanzado(flowPanelNegociacion, "Negociación");
        }

        private void btnFiltroCierre_Click(object sender, EventArgs e)
        {
            AbrirFiltroAvanzado(flowPanelCierre, "Cierre");
        }


        private void btnResetFiltrosCalificacion_Click(object sender, EventArgs e)
        {
            ResetearFiltros(flowPanelCalificacion, "Calificación");
        }

        private void btnResetFiltrosAsesoria_Click(object sender, EventArgs e)
        {
            ResetearFiltros(flowPanelAsesoria, "Asesoría");
        }

        private void btnResetFiltrosNegociacion_Click(object sender, EventArgs e)
        {
            ResetearFiltros(flowPanelNegociacion, "Negociación");
        }

        private void btnResetFiltrosCierre_Click(object sender, EventArgs e)
        {
            ResetearFiltros(flowPanelCierre, "Cierre");
        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }
    }
}