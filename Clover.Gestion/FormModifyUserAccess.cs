using Clover.DbLayer;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class FormModifyUserAccess : Form
    {
        public FormModifyUserAccess()
        {
            InitializeComponent();
            LoadUsers();
            LoadAccessLevels();
        }

        // Método para cargar los usuarios al ComboBox
        private void LoadUsers()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT UserID, UserName FROM user";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int userId = reader.GetInt32("UserID");
                        string userName = reader.GetString("UserName");
                        cboUsers.Items.Add(new { Text = userName, Value = userId });
                    }

                    cboUsers.DisplayMember = "Text";
                    cboUsers.ValueMember = "Value";
                    cboUsers.SelectedIndex = 0; // Selecciona el primer usuario por defecto
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message);
            }
        }

        // Método para cargar los niveles de acceso al ComboBox
        private void LoadAccessLevels()
        {
            cboAccessLevel.Items.Add(new { Text = "Invitado", Value = 1 });
            cboAccessLevel.Items.Add(new { Text = "Usuario", Value = 2 });
            cboAccessLevel.Items.Add(new { Text = "Usuario Avanzado", Value = 3 });
            cboAccessLevel.Items.Add(new { Text = "Supervisor", Value = 4 });
            cboAccessLevel.Items.Add(new { Text = "Administrador", Value = 5 });

            cboAccessLevel.DisplayMember = "Text";
            cboAccessLevel.ValueMember = "Value";
            cboAccessLevel.SelectedIndex = 0; // Selecciona el primer nivel de acceso por defecto
        }

        // Evento que se dispara al hacer clic en el botón de modificar acceso
        private void btnModifyAccess_Click(object sender, EventArgs e)
        {
            if (cboUsers.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un usuario.");
                return;
            }

            int userId = (int)((dynamic)cboUsers.SelectedItem).Value;
            int accessLevel = (int)((dynamic)cboAccessLevel.SelectedItem).Value;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE user SET AccessLevel = @AccessLevel WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@AccessLevel", accessLevel);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Nivel de acceso modificado correctamente.");
                cboUsers.SelectedIndex = 0;
                cboAccessLevel.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el nivel de acceso: " + ex.Message);
            }
        }
    }
}
