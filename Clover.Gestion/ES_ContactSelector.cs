using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ES_ContactSelector : Form
    {
        public List<CustomerContact> SelectedContacts = null;

        public ES_ContactSelector(List<CustomerContact> contacts)
        {
            InitializeComponent();
            clbxContacts.DataSource = contacts;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SelectedContacts = clbxContacts.CheckedItems.Cast<CustomerContact>().ToList();
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void clbxContacts_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((CustomerContact)e.ListItem).ContactName;
        }
    }
}
