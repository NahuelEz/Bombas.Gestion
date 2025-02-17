using System;
using System.Windows.Forms;

namespace Clover.TabletApp
{
    public partial class QuickSearch : Form
    {
        public string SearchInput = null;

        public QuickSearch()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    {
                        btnAccept.PerformClick();
                        return true;
                    }
                case Keys.Escape:
                    {
                        btnCancel.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchInput.Text))
            {
                MessageBox.Show("Por favor, complete el campo de búsqueda.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SearchInput = txtSearchInput.Text.Trim();
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
