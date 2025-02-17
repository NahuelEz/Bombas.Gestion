using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ES_PercentageAdjustment : Form
    {
        public int Percentage;

        public ES_PercentageAdjustment()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Percentage = (int)(nudPercentage.Value);
            this.DialogResult = DialogResult.OK;
        }
    }
}
