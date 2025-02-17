using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CH_CreateGroup : Form
    {
        private List<User> availableUsers;

        public CH_CreateGroup()
        {
            InitializeComponent();
            LoadAvailableUsers();
        }

        private void LoadAvailableUsers()
        {
            try
            {
                availableUsers = User.GetUsersByAccessLevel(4); // Cargar usuarios con nivel de acceso 4
                foreach (var user in availableUsers)
                {
                    clbUsers.Items.Add(user.UserName, false); // clbUsers es un CheckedListBox
                }
            }
            catch (Exception dbException)
            {
                MessageBox.Show("Error al cargar usuarios."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception al cargar usuarios. Message: " + dbException.Message);
            }
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string groupName = txtGroupName.Text.Trim();

            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("Por favor, ingresa un nombre para el grupo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clbUsers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos un usuario para el grupo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUsers = clbUsers.CheckedItems.Cast<string>().ToList();
            var selectedUserIDs = availableUsers
                .Where(u => selectedUsers.Contains(u.UserName))
                .Select(u => u.UserID)
                .ToList();

            try
            {
                var newGroup = new ChatGroup()
                {
                    GroupName = groupName,
                    CreatedByUserID = AppEnvironment.CurrentUser.UserID,
                    CreationDate = DateTime.Now
                };
                newGroup.Insert();

                foreach (var userID in selectedUserIDs)
                {
                    var groupMember = new ChatGroupMember()
                    {
                        GroupID = newGroup.GroupID,
                        UserID = userID
                    };
                    groupMember.Insert();
                }

                MessageBox.Show("Grupo creado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el grupo."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception al crear grupo. Message: " + ex.Message);
            }
        }
    }
}
