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
    public partial class FormChatWindow : Form
    {
        private int groupId;

        public FormChatWindow(int groupId)
        {
            InitializeComponent();
            this.groupId = groupId;
            LoadMessages();
        }

        private void LoadMessages()
        {
            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserName, Message, Timestamp FROM GroupChatMessages JOIN user ON GroupChatMessages.UserID = user.UserID WHERE GroupChatID = @GroupChatID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupChatID", groupId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string userName = reader.GetString("UserName");
                            string message = reader.GetString("Message");
                            DateTime timestamp = reader.GetDateTime("Timestamp");
                            string displayMessage = $"{timestamp} - {userName}: {message}";
                            listBoxChatMessages.Items.Add(displayMessage);
                        }
                    }
                }
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Por favor, ingrese un mensaje.");
                return;
            }

            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO GroupChatMessages (GroupChatID, UserID, Message, Timestamp) VALUES (@GroupChatID, @UserID, @Message, @Timestamp)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupChatID", groupId);
                    command.Parameters.AddWithValue("@UserID", DbLayerSettings.UserID);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }

            listBoxChatMessages.Items.Add($"Yo: {message}");
            txtMessage.Clear();
        }
    }
}