using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SHA_TextBox : Form
    {
        public string EditedText = null;

        public SHA_TextBox(string input, int maxLenght)
        {
            InitializeComponent();
            txtEditBox.Text = input;
            txtEditBox.MaxLength = maxLenght;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            EditedText = txtEditBox.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
