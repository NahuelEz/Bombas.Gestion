using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CU_ContactManager : Form
    {
        public BindingList<CustomerContact> Contacts = null;

        public CU_ContactManager(List<CustomerContact> Contacts)
        {
            InitializeComponent();
            this.Contacts = new BindingList<CustomerContact>(Contacts);
            dgvContacts.AutoGenerateColumns = false;
            dgvContacts.DataSource = this.Contacts;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddContact_Click(object sender, EventArgs e)
        {
            using (var form = new CU_ContactManager_Contact())
            {
                form.ShowDialog(this);
            }
        }

        private void dgvContacts_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona fila cuando se hace click con botón derecho.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvContacts.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvContacts.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private void cmsItemEditContact_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedContact = (CustomerContact)dgvContacts.SelectedRows[0].DataBoundItem;
            using (var form = new CU_ContactManager_Contact(selectedContact))
            {
                form.ShowDialog(this);
            }
        }
        private void cmsItemDeleteContact_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0)
            {
                return;
            }
            Contacts.RemoveAt(dgvContacts.SelectedRows[0].Index);
        }
    }
}
