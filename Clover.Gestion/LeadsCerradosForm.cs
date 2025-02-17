using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Clover.DbLayer;

namespace Clover.Gestion
{
    public partial class LeadsCerradosForm : Form
    {
        public LeadsCerradosForm()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            CargarLeadsCerrados();
        }

        private void ConfigurarDataGridView()
        {
            // Configurar DataGridView
            dgvLeadsCerrados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLeadsCerrados.ReadOnly = true;
            dgvLeadsCerrados.AllowUserToAddRows = false;
            dgvLeadsCerrados.AllowUserToDeleteRows = false;
            dgvLeadsCerrados.RowHeadersVisible = false;
        }

        private void CargarLeadsCerrados()
        {
            DataTable leadsCerrados = new DataTable();

            // Consulta SQL para obtener los leads cerrados
            string query = @"
        SELECT 
            l.LeadID AS 'ID',
            l.Nombre AS 'Nombre',
            lc.EstadoCliente AS 'Estado del Cliente',
            lc.TipoFacturacion AS 'Tipo de Facturación',
            lc.FormaPago AS 'Forma de Pago',
            lc.FechaCierre AS 'Fecha de Cierre',
            hc.Producto AS 'Producto',
            hc.Cantidad AS 'Cantidad Comprada',
            hc.Total AS 'Total Compra'
        FROM Leads l
        LEFT JOIN LeadsCierre lc ON l.LeadID = lc.LeadID
        LEFT JOIN HistorialCompras hc ON l.LeadID = hc.LeadID
        WHERE l.Estado = 'Cerrado' -- Cambiado a 'Cerrado'
        ORDER BY lc.FechaCierre DESC";

            // Ejecutar la consulta
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(leadsCerrados);
                        }
                    }
                }

                // Asignar datos al DataGridView
                dgvLeadsCerrados.DataSource = leadsCerrados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los leads cerrados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
