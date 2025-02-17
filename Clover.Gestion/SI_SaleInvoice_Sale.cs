using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SI_SaleInvoice_Sale : Form
    {
        public List<Sale> SelectedSales = new List<Sale>();

        private int CustomerID;
        private IList<Sale> CurrentSales;

        public SI_SaleInvoice_Sale(int CustomerID, IList<Sale> CurrentSales)
        {
            this.CustomerID = CustomerID;
            this.CurrentSales = CurrentSales;
            InitializeComponent();
        }

        private async void SI_SaleInvoice_Sale_Load(object sender, EventArgs e)
        {
            List<Sale> salesFromCustomer;
            try
            {
                salesFromCustomer = await Task.Run(() => Sale.GetSalesByCustomerId(CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint SI301
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SI301 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            var unselectedSales = salesFromCustomer.Where(s => !s.IsUnmarked && !CurrentSales.Any(x => x.SaleID == s.SaleID)).ToList();
            clbxAssociatedSales.DataSource = unselectedSales;
        }

        private void clbxAssociatedSales_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = $"ID: {((Sale)e.ListItem).SaleID:D4}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (clbxAssociatedSales.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedSales = clbxAssociatedSales.CheckedItems.Cast<Sale>().ToList();
            this.DialogResult = DialogResult.OK;
        }
    }
}
