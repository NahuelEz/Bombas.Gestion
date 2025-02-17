using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;
using Clover.DbLayer;
using System.Linq;

namespace Clover.Gestion
{
    public partial class CalificarForm : Form
    {
        private int LeadID;
        private string LeadName;

        public CalificarForm(int leadID, string leadName)
        {
            InitializeComponent();
            LeadID = leadID;
            LeadName = leadName;
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configuración del formulario
            this.Text = "Calificar Lead";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;

            // Etiqueta para nombre del Lead
            Label lblLead = new Label
            {
                Text = $"Lead: {LeadName}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(lblLead);

            // Etiqueta para nivel de urgencia
            Label lblUrgencia = new Label
            {
                Text = "Nivel de Urgencia:",
                AutoSize = true,
                Location = new Point(20, 60)
            };
            this.Controls.Add(lblUrgencia);

            // ComboBox para seleccionar nivel de urgencia
            ComboBox cmbUrgencia = new ComboBox
            {
                Name = "cmbUrgencia",
                Location = new Point(150, 55),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbUrgencia.Items.AddRange(new string[] { "Alta", "Media", "Baja" }); // Ajuste según valores válidos en la base de datos
            this.Controls.Add(cmbUrgencia);

            // Etiqueta para notas
            Label lblNotas = new Label
            {
                Text = "Notas:",
                AutoSize = true,
                Location = new Point(20, 100)
            };
            this.Controls.Add(lblNotas);

            // TextBox para agregar notas
            TextBox txtNotas = new TextBox
            {
                Name = "txtNotas",
                Location = new Point(150, 95),
                Width = 200,
                Height = 80,
                Multiline = true
            };
            this.Controls.Add(txtNotas);

            // Botón Guardar
            Button btnGuardar = new Button
            {
                Text = "Guardar",
                Location = new Point(150, 200),
                Width = 100
            };
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener los valores ingresados por el usuario
            string nivelUrgencia = ((ComboBox)this.Controls["cmbUrgencia"]).Text;
            string notas = ((TextBox)this.Controls["txtNotas"]).Text;

            if (string.IsNullOrEmpty(nivelUrgencia))
            {
                MessageBox.Show("Por favor, seleccione un nivel de urgencia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que el nivel de urgencia sea válido
            if (!new[] { "Alta", "Media", "Baja" }.Contains(nivelUrgencia))
            {
                MessageBox.Show("El nivel de urgencia seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insertar calificación en la base de datos
            GuardarCalificacionEnBaseDeDatos(nivelUrgencia, notas);

            // Cerrar el formulario
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GuardarCalificacionEnBaseDeDatos(string nivelUrgencia, string notas)
        {
            string query = @"INSERT INTO calificaciones (LeadID, NivelUrgencia, FechaCalificacion, Notas) 
                             VALUES (@LeadID, @NivelUrgencia, @FechaCalificacion, @Notas)";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", LeadID);
                    cmd.Parameters.AddWithValue("@NivelUrgencia", nivelUrgencia);
                    cmd.Parameters.AddWithValue("@FechaCalificacion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Notas", notas);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Calificación guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
