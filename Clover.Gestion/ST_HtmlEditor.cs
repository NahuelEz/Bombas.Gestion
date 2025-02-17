using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ST_HtmlEditor : Form
    {
        public string HtmlContent = null;

        public ST_HtmlEditor(string input)
        {
            InitializeComponent();
            htmMainEditor.SetDocumentHtml(input);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            HtmlContent = htmMainEditor.GetDocumentHtml();
            DialogResult = DialogResult.OK;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
