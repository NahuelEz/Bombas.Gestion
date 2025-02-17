using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clover.DbLayer;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    public partial class FiltrosAvanzadosForm : Form
    {
        public string FiltroNombre { get; private set; }
        public string FiltroUrgencia { get; private set; }
        public DateTime? FiltroFecha { get; private set; }

        public FiltrosAvanzadosForm()
        {
            InitializeComponent();

            
            cmbFiltroUrgencia.Items.AddRange(new string[] { "Bajo", "Medio", "Alto" });
            cmbFiltroUrgencia.SelectedIndex = -1; 

            
            dtpFiltroFecha.Checked = false;
            dtpFiltroFecha.ShowCheckBox = true;
        }



        private void AplicarFiltro(FlowLayoutPanel flowPanel, string estado, string nombre, string urgencia, DateTime? fecha)
        {
            DataTable leadsFiltrados = new DataTable();

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
                MessageBox.Show($"Error al filtrar los leads: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Limpiar y cargar los leads filtrados en el FlowLayoutPanel
            flowPanel.Controls.Clear();

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

                flowPanel.Controls.Add(card);
            }
        }




        private void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            // Solo pasar los valores si se han proporcionado
            FiltroNombre = string.IsNullOrWhiteSpace(txtFiltroNombre.Text) ? null : txtFiltroNombre.Text;
            FiltroUrgencia = cmbFiltroUrgencia.SelectedIndex >= 0 ? cmbFiltroUrgencia.SelectedItem.ToString() : null;
            FiltroFecha = dtpFiltroFecha.Checked ? dtpFiltroFecha.Value.Date : (DateTime?)null;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        // Repite para cada FlowLayoutPanel.

    }

}
