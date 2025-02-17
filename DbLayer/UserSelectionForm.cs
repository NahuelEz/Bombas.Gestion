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

namespace Clover.DbLayer
{
    public partial class UserSelectionForm : Form
    {
        public UserSelectionForm()
        {
            InitializeComponent();
            LoadUsers(); // Cargar usuarios al inicializar
        }

        private void LoadUsers()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT UserID, UserName FROM `user`";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int userID = reader.GetInt32("UserID");
                                string userName = reader.GetString("UserName");
                                listBoxUsers.Items.Add(new UserListItem(userID, userName));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                var selectedItem = (UserListItem)listBoxUsers.SelectedItem;
                txtNewUsername.Text = selectedItem.UserName;
            }
        }

        private void btnUpdateUsername_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                var selectedItem = (UserListItem)listBoxUsers.SelectedItem;
                string newUsername = txtNewUsername.Text;

                if (string.IsNullOrEmpty(newUsername))
                {
                    MessageBox.Show("Por favor, ingresa un nuevo nombre de usuario.");
                    return;
                }

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                    {
                        conn.Open();
                        string query = "UPDATE `user` SET `UserName` = @newUserName WHERE `UserID` = @userID";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@newUserName", newUsername);
                            cmd.Parameters.AddWithValue("@userID", selectedItem.UserID);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Nombre de usuario actualizado correctamente.");
                                // Actualizar el nombre en la lista
                                selectedItem.UserName = newUsername;
                                listBoxUsers.Items[listBoxUsers.SelectedIndex] = selectedItem;
                            }
                            else
                            {
                                MessageBox.Show("No se pudo actualizar el nombre de usuario.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
        }
    }

    public class UserListItem
    {
        public int UserID { get; }
        public string UserName { get; set; }

        public UserListItem(int userID, string userName)
        {
            UserID = userID;
            UserName = userName;
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}

