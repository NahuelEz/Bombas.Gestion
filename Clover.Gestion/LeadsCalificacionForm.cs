using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Clover.DbLayer;
using System.Linq;
using System.Collections.Generic;

namespace Clover.Gestion
{
    public partial class LeadsCalificacionForm : Form
    {
        public LeadsCalificacionForm()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CargarLeads(); // Cargar todos los leads al abrir el formulario
        }

        private void ConfigurarFormulario()
        {
            // Configurar DataGridView
            dgvLeadsCalificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLeadsCalificacion.ReadOnly = false; // Permitir edición
            dgvLeadsCalificacion.AllowUserToAddRows = false;
            dgvLeadsCalificacion.AllowUserToDeleteRows = false;
            dgvLeadsCalificacion.RowHeadersVisible = false;

            // Agregar ComboBox dinámico para "TipoCasilla"
            DataGridViewComboBoxColumn tipoCasillaColumn = new DataGridViewComboBoxColumn
            {
                Name = "TipoCasilla",
                HeaderText = "Tipo de Casilla",
                DataPropertyName = "TipoCasilla",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            };

            // Cargar valores únicos de "TipoCasilla" desde la base de datos
            tipoCasillaColumn.Items.AddRange(ObtenerValoresUnicosDeCasilla());

            // Reemplazar columna existente con la columna ComboBox
            if (dgvLeadsCalificacion.Columns.Contains("TipoCasilla"))
                dgvLeadsCalificacion.Columns.Remove("TipoCasilla");

            dgvLeadsCalificacion.Columns.Add(tipoCasillaColumn);

            // Asociar eventos
            dgvLeadsCalificacion.CellValueChanged += dgvLeadsCalificacion_CellValueChanged;
            dgvLeadsCalificacion.CellValidating += dgvLeadsCalificacion_CellValidating;
            dgvLeadsCalificacion.DataError += dgvLeadsCalificacion_DataError;
        }

        private string[] ObtenerValoresUnicosDeCasilla()
        {
            List<string> valores = new List<string>
    {
        "Particular",
        "Particular Industrial",
        "Industrial Primera Compra",
        "Industrial Habitual",
        "Industrial VIP",
        "Licitación"
    };

            // Agregar valores únicos desde la base de datos
            try
            {
                string query = "SELECT DISTINCT TipoCasilla FROM Leads WHERE Estado = 'Calificación'";
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tipoCasilla = reader["TipoCasilla"].ToString();
                                if (!valores.Contains(tipoCasilla))
                                    valores.Add(tipoCasilla);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los valores únicos de 'TipoCasilla': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valores.ToArray();
        }




        private void CargarLeads(string nombre = null, string tipoCasilla = null)
        {
            DataTable leads = new DataTable();

            string query = "SELECT LeadID, Nombre, TipoCasilla, FechaCalificacion, NivelUrgencia, Nota FROM Leads WHERE Estado = 'Calificación'";

            if (!string.IsNullOrEmpty(nombre))
                query += " AND Nombre LIKE @Nombre";
            if (!string.IsNullOrEmpty(tipoCasilla) && tipoCasilla != "Todas")
                query += " AND TipoCasilla = @TipoCasilla";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(nombre))
                            cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                        if (!string.IsNullOrEmpty(tipoCasilla) && tipoCasilla != "Todas")
                            cmd.Parameters.AddWithValue("@TipoCasilla", tipoCasilla);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(leads);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los leads: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asignar los datos al DataGridView
            dgvLeadsCalificacion.DataSource = leads;

            // Deshabilitar edición en columnas específicas
            dgvLeadsCalificacion.Columns["LeadID"].ReadOnly = true;
            dgvLeadsCalificacion.Columns["FechaCalificacion"].ReadOnly = true;
        }

        private void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscarNombre.Text;
            string tipoCasilla = cmbFiltrarCasilla.SelectedItem.ToString();
            CargarLeads(nombre, tipoCasilla); // Aplicar filtros
        }

        private void btnResetearFiltros_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            cmbFiltrarCasilla.SelectedIndex = 0;
            CargarLeads(); // Recargar todos los leads sin filtros
        }

        private void dgvLeadsCalificacion_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que el índice sea válido
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    // Obtener valores actualizados
                    int leadId = Convert.ToInt32(dgvLeadsCalificacion.Rows[e.RowIndex].Cells["LeadID"].Value);
                    string columnaModificada = dgvLeadsCalificacion.Columns[e.ColumnIndex].Name;
                    string nuevoValor = dgvLeadsCalificacion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

                    // Actualizar la base de datos
                    ActualizarLeadEnBaseDatos(leadId, columnaModificada, nuevoValor);

                    // Si la columna modificada es "TipoCasilla", también actualizamos la FechaCalificacion
                    if (columnaModificada == "TipoCasilla")
                    {
                        string fechaActual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dgvLeadsCalificacion.Rows[e.RowIndex].Cells["FechaCalificacion"].Value = fechaActual; // Actualizar en el DataGridView
                        ActualizarLeadEnBaseDatos(leadId, "FechaCalificacion", fechaActual); // Guardar en la base de datos
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el lead: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarLeadEnBaseDatos(int leadId, string columna, string nuevoValor)
        {
            string query = $"UPDATE Leads SET {columna} = @NuevoValor WHERE LeadID = @LeadID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NuevoValor", nuevoValor);
                        cmd.Parameters.AddWithValue("@LeadID", leadId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("Incorrect datetime value"))
            {
                // Si ocurre un error de formato de fecha, omitir el mensaje si ya se guardó
                MessageBox.Show($"El valor ya fue guardado.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLeadsCalificacion_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Validar la columna "TipoCasilla"
            if (dgvLeadsCalificacion.Columns[e.ColumnIndex].Name == "TipoCasilla")
            {
                var opcionesValidas = new List<string>
        {
            "Particular",
            "Particular Industrial",
            "Industrial Primera Compra",
            "Industrial Habitual",
            "Industrial VIP",
            "Licitación"
        };

                if (!opcionesValidas.Contains(e.FormattedValue.ToString()))
                {
                    MessageBox.Show("Por favor, seleccione un valor válido para 'Tipo de Casilla'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Cancelar la edición
                }
            }
        }


        private void dgvLeadsCalificacion_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Error de datos: Asegúrese de seleccionar un valor válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false; // Evitar que se lance una excepción
        }



    }
}
