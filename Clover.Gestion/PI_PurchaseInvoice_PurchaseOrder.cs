using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PI_PurchaseInvoice_PurchaseOrder : Form
    {
        public PurchaseOrder[] SelectedPurchaseOrders = null;

        private int ProviderID;
        private int? PurchaseInvoiceID;
        private IList<PurchaseOrder> CurrentPurchaseOrders;

        public PI_PurchaseInvoice_PurchaseOrder(int ProviderID, int? PurchaseInvoiceID, IList<PurchaseOrder> CurrentPurchaseOrders)
        {
            this.ProviderID = ProviderID;
            this.PurchaseInvoiceID = PurchaseInvoiceID;
            this.CurrentPurchaseOrders = CurrentPurchaseOrders;
            InitializeComponent();
        }

        private async void PI_PurchaseInvoice_PurchaseOrder_Load(object sender, EventArgs e)
        {
            List<PurchaseOrder> orders;
            try
            {
                orders = await Task.Run(() => PurchaseOrder.GetPurchaseOrdersByProviderIdAndPurchaseInvoiceId(ProviderID, PurchaseInvoiceID));
            }
            catch (Exception dbException)
            {
                // Waypoint PI301
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PI301 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            var unselectedPurchaseOrders = orders.Where(x => CurrentPurchaseOrders.All(y => y.PurchaseOrderID != x.PurchaseOrderID)).ToArray();
            clbxPurchaseOrders.DataSource = unselectedPurchaseOrders;
        }

        private void clbxPurchaseOrders_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = $"ID: {((PurchaseOrder)e.ListItem).PurchaseOrderID:D4}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (clbxPurchaseOrders.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una orden de compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedPurchaseOrders = clbxPurchaseOrders.CheckedItems.Cast<PurchaseOrder>().ToArray();
            this.DialogResult = DialogResult.OK;
        }
    }
}
