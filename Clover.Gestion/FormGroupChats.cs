using Clover.DbLayer;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class FormGroupChats : Form
    {
        public FormGroupChats()
        {
            InitializeComponent();
        }

        private void FormGroupChats_Load(object sender, EventArgs e)
        {
            ControlAccessBasedOnUserLevel(); // Ocultar botones según el nivel de acceso
            LoadGroupChats();
        }

        private void LoadGroupChats()
        {
            string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT GroupChatID, GroupName FROM GroupChat WHERE IsDeleted = 0"; // Solo grupos no eliminados
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        listBoxGroupChats.Items.Clear();
                        while (reader.Read())
                        {
                            string groupName = reader.GetString("GroupName");
                            int groupId = reader.GetInt32("GroupChatID");
                            listBoxGroupChats.Items.Add(new GroupChat { GroupChatID = groupId, GroupName = groupName });
                        }
                    }
                }
            }
        }

        private void btnOpenChat_Click_Click(object sender, EventArgs e)
        {
            if (listBoxGroupChats.SelectedItem is GroupChat selectedChat)
            {
                FormChatWindow chatWindow = new FormChatWindow(selectedChat.GroupChatID);
                chatWindow.Show();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un grupo.");
            }
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroupChats.SelectedItem is GroupChat selectedChat)
            {
                using (FormEditGroupName editGroupNameForm = new FormEditGroupName(selectedChat.GroupName))
                {
                    if (editGroupNameForm.ShowDialog() == DialogResult.OK)
                    {
                        string newGroupName = editGroupNameForm.NewGroupName;

                        if (!string.IsNullOrEmpty(newGroupName))
                        {
                            string connectionString = DbLayerSettings.ConnectionString;

                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                connection.Open();
                                string query = "UPDATE GroupChat SET GroupName = @GroupName WHERE GroupChatID = @GroupChatID";
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@GroupName", newGroupName);
                                    command.Parameters.AddWithValue("@GroupChatID", selectedChat.GroupChatID);
                                    command.ExecuteNonQuery();
                                }
                            }

                            // Refresh the list
                            LoadGroupChats();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un grupo.");
            }
        }

        private async void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroupChats.SelectedItem is GroupChat selectedChat)
            {
                DialogResult result = MessageBox.Show(
                    "¿Estás seguro de que deseas ocultar este grupo de chat? Esta acción marcará el grupo como eliminado y lo ocultará de la lista.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    string connectionString = DbLayerSettings.ConnectionString;

                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            await connection.OpenAsync();

                            string updateQuery = "UPDATE groupchat SET IsDeleted = 1 WHERE GroupChatID = @GroupChatID";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@GroupChatID", selectedChat.GroupChatID);
                                await updateCommand.ExecuteNonQueryAsync();
                            }

                            // Eliminar el grupo de la lista
                            listBoxGroupChats.Items.Remove(selectedChat);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el grupo: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un grupo para ocultar.");
            }
        }

        // Evento para el botón que eliminará usuarios del grupo seleccionado
        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (listBoxGroupChats.SelectedItem is GroupChat selectedChat)
            {
                using (FormRemoveUser formRemoveUser = new FormRemoveUser(selectedChat.GroupChatID))
                {
                    if (formRemoveUser.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Usuarios eliminados correctamente.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un grupo.");
            }
        }

        private void ControlAccessBasedOnUserLevel()
        {
            int accessLevel = AppEnvironment.CurrentUser.AccessLevel;

            if (accessLevel < 5)
            {
                btnEditGroup.Visible = false;
                btnDeleteGroup.Visible = false;
                btnRemoveUser.Visible = false;
            }
        }

        private class GroupChat
        {
            public int GroupChatID { get; set; }
            public string GroupName { get; set; }

            public override string ToString()
            {
                return GroupName;
            }
        }
    }
}
