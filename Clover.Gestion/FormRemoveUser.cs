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
    public partial class FormRemoveUser : Form
    {
        private ListBox listBoxUsers;
        private Button btnRemove;
        private int groupChatID;

        public FormRemoveUser(int groupChatID)
        {
            this.groupChatID = groupChatID;

            listBoxUsers = new ListBox { Dock = DockStyle.Top, Height = 200 };
            btnRemove = new Button { Text = "Eliminar Usuario(s)", Dock = DockStyle.Bottom };

            btnRemove.Click += BtnRemove_Click;

            Controls.Add(listBoxUsers);
            Controls.Add(btnRemove);

            LoadUsers();
        }

        private void LoadUsers()
        {
            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT u.UserID, u.UserName
            FROM groupchatmembers gm
            INNER JOIN user u ON gm.UserID = u.UserID
            WHERE gm.GroupChatID = @GroupChatID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupChatID", groupChatID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        listBoxUsers.Items.Clear();
                        while (reader.Read())
                        {
                            int userID = reader.GetInt32("UserID");
                            string userName = reader.GetString("UserName");

                            // Agregamos el usuario al ListBox mostrando su nombre
                            listBoxUsers.Items.Add(new User { UserID = userID, UserName = userName });
                        }
                    }
                }
            }
        }



        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItems.Count > 0)
            {
                string connectionString = DbLayerSettings.ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (User user in listBoxUsers.SelectedItems)
                    {
                        string query = "DELETE FROM GroupChatMembers WHERE GroupChatID = @GroupChatID AND UserID = @UserID";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@GroupChatID", groupChatID);
                            command.Parameters.AddWithValue("@UserID", user.UserID);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                LoadUsers();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione al menos un usuario para eliminar.");
            }
        }

        private class User
        {
            public int UserID { get; set; }
            public string UserName { get; set; }

            public override string ToString()
            {
                return UserName;
            }
        }
    }
}
