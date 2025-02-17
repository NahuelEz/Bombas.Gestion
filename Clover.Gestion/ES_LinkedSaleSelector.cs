using Clover.DbLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ES_LinkedSaleSelector : Form
    {
        public int? SelectedSaleID = null;

        public ES_LinkedSaleSelector(List<Sale> linkedSales)
        {
            InitializeComponent();
            lbxLinkedSales.DataSource = linkedSales;
        }

        private void lbxLinkedSales_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((Sale)e.ListItem).SaleID.ToString("D8");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (lbxLinkedSales.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una venta para continuar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedSaleID = ((Sale)lbxLinkedSales.SelectedItem).SaleID;
            this.DialogResult = DialogResult.OK;
        }
    }
}
