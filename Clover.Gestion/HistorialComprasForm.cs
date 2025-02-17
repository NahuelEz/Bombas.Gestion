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
    public partial class HistorialComprasForm : Form
    {
        private int LeadID;

        public HistorialComprasForm(int leadId)
        {
            InitializeComponent();
            LeadID = leadId;
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            DataTable historial = new DataTable();
            string query = "SELECT FechaCompra, Producto, Cantidad, Precio, Total, Notas FROM HistorialCompras WHERE LeadID = @LeadID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeadID", LeadID);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(historial);
                        }
                    }
                }

                dgvHistorialCompras.DataSource = historial;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial de compras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarCompra_Click(object sender, EventArgs e)
        {
            using (AgregarCompraForm form = new AgregarCompraForm(LeadID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    CargarHistorial();
                }
            }
        }
    }
}
