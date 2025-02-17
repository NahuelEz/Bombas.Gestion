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
    public partial class FormAddUsersToGroup : Form
    {
        private int groupId;

        public FormAddUsersToGroup(int groupId)
        {
            InitializeComponent();
            this.groupId = groupId;
            LoadUsers();
        }

        private void LoadUsers()
        {
            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserID, UserName FROM user";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clbUsers.Items.Add(new User { UserID = reader.GetInt32("UserID"), UserName = reader.GetString("UserName") });
                        }
                    }
                }
            }
        }

        private void btnAddSelectedUsers_Click(object sender, EventArgs e)
        {
            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                foreach (User user in clbUsers.CheckedItems)
                {
                    string query = "INSERT INTO GroupChatMembers (GroupChatID, UserID) VALUES (@GroupChatID, @UserID)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GroupChatID", groupId);
                        command.Parameters.AddWithValue("@UserID", user.UserID);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Usuarios añadidos al grupo exitosamente.");
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

        private void FormAddUsersToGroup_Load(object sender, EventArgs e)
        {

        }
    }
}
