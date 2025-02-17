using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Clover.DbLayer;

namespace Clover.Gestion
{
    public partial class DelegarForm : Form
    {
        private int LeadID; // ID del Lead que se está delegando

        public DelegarForm(int leadID)
        {
            InitializeComponent();
            LeadID = leadID;
            CargarUsuariosDisponibles();
        }

        private void CargarUsuariosDisponibles()
        {
            try
            {
                // Crear la consulta SQL para obtener todos los usuarios
                string query = @"
                    SELECT UserID, UserName 
                    FROM `user`";

                // Limpiar los elementos existentes en el ComboBox
                cmbUsuarios.Items.Clear();

                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Verificar si hay datos en la tabla
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("No se encontraron usuarios disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            while (reader.Read())
                            {
                                // Agregar los usuarios al ComboBox
                                cmbUsuarios.Items.Add(new ComboboxItem
                                {
                                    Text = reader["UserName"].ToString(),
                                    Value = reader["UserID"].ToString()
                                });
                            }
                        }
                    }
                }

                if (cmbUsuarios.Items.Count > 0)
                {
                    cmbUsuarios.SelectedIndex = 0; // Seleccionar el primer usuario por defecto
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelegar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un usuario
            if (cmbUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario para delegar el lead.");
                return;
            }

            // Obtener el UserID seleccionado
            var selectedUser = (ComboboxItem)cmbUsuarios.SelectedItem;
            int userID = int.Parse(selectedUser.Value);

            // Llamar a la función para actualizar la base de datos
            DelegarLead(LeadID, userID);

            // Mostrar mensaje de confirmación
            MessageBox.Show("Lead delegado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Cerrar el formulario
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DelegarLead(int leadID, int userID)
        {
            try
            {
                // Actualización de la consulta con el nombre correcto de las columnas
                string query = "UPDATE Leads SET UsuarioID = @UserID WHERE LeadID = @LeadID";

                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Configuración de los parámetros con los nombres correctos
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@LeadID", leadID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al delegar el lead: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Clase para manejar elementos de ComboBox con valores asociados
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text; // Mostrar el texto en el ComboBox
        }
    }
}
