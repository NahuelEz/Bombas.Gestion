using Clover.DbLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class FormChangePassword : Form
    {
        public FormChangePassword()
        {
            InitializeComponent();
            LoadUsers();
        }

        // Método para cargar los usuarios desde la base de datos y mostrarlos en el DataGridView
        private void LoadUsers()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT UserID, UserName, Password, AccessLevel FROM user";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message);
            }
        }

        // Evento que se dispara al hacer clic en el botón de cambiar contraseña
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                lblMessage.Text = "Selecciona un usuario para cambiar la contraseña.";
                return;
            }

            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Validaciones básicas para los campos de contraseña
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblMessage.Text = "Las contraseñas no pueden estar vacías.";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblMessage.Text = "Las contraseñas no coinciden.";
                return;
            }

            // Obtenemos el UserID del usuario seleccionado
            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

            // Codificar la nueva contraseña a Base64 antes de guardarla
            string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(newPassword));

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "UPDATE user SET Password = @Password WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Password", encodedPassword);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Contraseña cambiada correctamente.";
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cambiar la contraseña: " + ex.Message;
            }
        }
    }
}
