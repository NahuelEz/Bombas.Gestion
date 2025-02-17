using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SHA_QuickSearch : Form
    {
        public string SearchInput = null;

        public SHA_QuickSearch(string searchLabel)
        {
            InitializeComponent();
            label1.Text = $"Buscar por {searchLabel}:";
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
    }
}
