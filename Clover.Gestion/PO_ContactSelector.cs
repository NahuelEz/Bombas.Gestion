using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PO_ContactSelector : Form
    {
        public List<ProviderContact> SelectedContacts = null;

        public PO_ContactSelector(List<ProviderContact> contacts)
        {
            InitializeComponent();
            clbxContacts.DataSource = contacts;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SelectedContacts = clbxContacts.CheckedItems.Cast<ProviderContact>().ToList();
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void clbxContacts_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((ProviderContact)e.ListItem).ContactName;
        }
    }
}
