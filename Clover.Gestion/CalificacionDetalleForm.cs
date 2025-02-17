using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Clover.DbLayer;

namespace Clover.Gestion
{
    public partial class CalificacionDetalleForm : Form
    {
        private int LeadID; // ID del lead que se está calificando

        public CalificacionDetalleForm(int leadId)
        {
            InitializeComponent();
            LeadID = leadId;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que se seleccionó un tipo de casilla
            if (string.IsNullOrEmpty(cmbTipoCasilla.Text))
            {
                MessageBox.Show("Por favor, seleccione un tipo de casilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guardar en la base de datos
            try
            {
                GuardarCalificacion(LeadID, cmbTipoCasilla.Text);
                this.DialogResult = DialogResult.OK; // Indicar que se guardó correctamente
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la calificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Indicar que se canceló
            this.Close();
        }

        private void GuardarCalificacion(int leadId, string tipoCasilla)
        {
            string query = @"UPDATE Leads 
                             SET TipoCasilla = @TipoCasilla, 
                                 FechaCalificacion = @FechaCalificacion 
                             WHERE LeadID = @LeadID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TipoCasilla", tipoCasilla);
                    cmd.Parameters.AddWithValue("@FechaCalificacion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@LeadID", leadId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Calificación guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
