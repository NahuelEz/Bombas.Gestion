using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clover.HtmlEditor
{
    public partial class FontSelector : Form
    {
        public string SelectedFontFamily = null;
        public int? SelectedFontSize = null;

        public FontSelector()
        {
            InitializeComponent();
        }

        private void FontSelector_Load(object sender, EventArgs e)
        {
            lbxFontFamily.DataSource = FontFamily.Families.Select(f => f.Name).ToList();
            lbxFontSize.DataSource = new int[] { 1, 2, 3, 4, 5, 6, 7 }.ToList();
            if (SelectedFontFamily != null)
            {
                lbxFontFamily.SelectedItem = SelectedFontFamily;
            }
            if (SelectedFontSize != null)
            {
                lbxFontSize.SelectedItem = SelectedFontSize;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SelectedFontFamily = (string)lbxFontFamily.SelectedItem;
            SelectedFontSize = (int)lbxFontSize.SelectedItem;
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
