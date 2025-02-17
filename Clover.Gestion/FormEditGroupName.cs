using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class FormEditGroupName : Form
    {
        public string NewGroupName { get; private set; }

        public FormEditGroupName(string currentGroupName)
        {
            InitializeComponent();
            txtNewGroupName.Text = currentGroupName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NewGroupName = txtNewGroupName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormEditGroupName_Load(object sender, EventArgs e)
        {

        }
    }
}
