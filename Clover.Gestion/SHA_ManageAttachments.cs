using System;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SHA_ManageAttachments : Form
    {
        public string[] Attachments;

        public SHA_ManageAttachments(string[] Attachments)
        {
            this.Attachments = Attachments;
            InitializeComponent();
            lbxAttachments.Items.AddRange(Attachments);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    lbxAttachments.Items.Add(ofd.FileName);
                    Attachments = lbxAttachments.Items.Cast<string>().ToArray();
                }
            }
        }
        private void btnRemoveAttachment_Click(object sender, EventArgs e)
        {
            if (lbxAttachments.SelectedIndex != -1)
            {
                lbxAttachments.Items.RemoveAt(lbxAttachments.SelectedIndex);
            }
            Attachments = lbxAttachments.Items.Cast<string>().ToArray();
        }
    }
}
