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
    public partial class FormDeleteUser : Form
    {
        public FormDeleteUser()
        {
            InitializeComponent();
            LoadUsers();
        }



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
                        int userID = reader.GetInt32("UserID");
                        string userName = reader.GetString("UserName");
                        cboUsers.Items.Add(new { Text = userName, Value = userID });
                    }

                    cboUsers.DisplayMember = "Text";
                    cboUsers.ValueMember = "Value";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message);
            }
        }


        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (cboUsers.SelectedItem == null)
            {
                lblMessage.Text = "Por favor, seleccione un usuario.";
                return;
            }

            int userID = (int)((dynamic)cboUsers.SelectedItem).Value;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM user WHERE UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Usuario eliminado correctamente.";
                        cboUsers.Items.Clear();
                        LoadUsers();
                    }
                    else
                    {
                        lblMessage.Text = "No se encontró el usuario para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al eliminar el usuario: " + ex.Message;
            }
        }




    }
}
