using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SHA_MailPreview : Form
    {
        public string To;
        public string CC;
        public string Subject;
        public string Message;
        public string[] Attachments = new string[] { };

        public SHA_MailPreview(string From, string To, string CC, string Subject, string Message)
        {
            this.To = To;
            this.CC = CC;
            this.Subject = Subject;
            this.Message = Message;
            InitializeComponent();
            txtFromAddress.Text = From;
            txtToAddress.Text = To;
            txtCCAddress.Text = CC;
            txtSubject.Text = Subject;
            htmMessageBody.SetDocumentHtml(Message);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtToAddress.Text))
            {
                MessageBox.Show("Por favor, complete correo electrónico del destinatario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.To = txtToAddress.Text;
            this.CC = txtCCAddress.Text;
            this.Subject = txtSubject.Text;
            this.Message = htmMessageBody.GetDocumentHtml();
            this.DialogResult = DialogResult.OK;
        }
        private void btnManageAttachments_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_ManageAttachments(Attachments))
            {
                form.ShowDialog();
                Attachments = form.Attachments;
            }
        }
    }
}
