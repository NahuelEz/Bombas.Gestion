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
    public partial class FormCreateGroupChat : Form
    {
        public FormCreateGroupChat()
        {
            InitializeComponent();
        }

        private void FormCreateGroupChat_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string groupName = txtGroupName.Text.Trim();
            if (string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("Por favor, ingrese el nombre del grupo.");
                return;
            }

            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO GroupChat (GroupName) VALUES (@GroupName)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupName", groupName);
                    command.ExecuteNonQuery();
                }

                // Obtener el ID del nuevo grupo
                string selectQuery = "SELECT LAST_INSERT_ID()";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    int newGroupId = Convert.ToInt32(command.ExecuteScalar());
                    MessageBox.Show($"Grupo creado exitosamente con ID: {newGroupId}");
                    // Aquí puedes abrir el formulario de agregar usuarios
                    FormAddUsersToGroup formAddUsers = new FormAddUsersToGroup(newGroupId);
                    formAddUsers.Show();
                }
            }
        }

        private void btnAddUsers_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
