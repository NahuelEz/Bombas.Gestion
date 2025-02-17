using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PP_PayOrder_PurchaseInvoice : Form
    {
        public PurchaseInvoice[] SelectedPurchaseInvoices = null;

        private int ProviderID;
        private int? PayOrderID;
        private IList<PurchaseInvoice> CurrentPurchaseInvoices;

        public PP_PayOrder_PurchaseInvoice(int ProviderID, int? PayOrderID, IList<PurchaseInvoice> CurrentPurchaseInvoices)
        {
            this.ProviderID = ProviderID;
            this.PayOrderID = PayOrderID;
            this.CurrentPurchaseInvoices = CurrentPurchaseInvoices;
            InitializeComponent();
        }

        private async void PP_PayOrder_PurchaseInvoice_Load(object sender, EventArgs e)
        {
            List<PurchaseInvoice> invoices;
            try
            {
                invoices = await Task.Run(() => PurchaseInvoice.GetInvoicesByProviderIdAndPayOrderId(ProviderID, PayOrderID));
            }
            catch (Exception dbException)
            {
                // Waypoint PP301
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP301 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            var unselectedPurchaseInvoices = invoices.Where(x => CurrentPurchaseInvoices.All(y => y.PurchaseInvoiceID != x.PurchaseInvoiceID)).ToArray();
            clbxPurchaseInvoices.DataSource = unselectedPurchaseInvoices;
        }

        private void clbxPurchaseInvoices_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = $"N°: {((PurchaseInvoice)e.ListItem).InvoiceNumber}";
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (clbxPurchaseInvoices.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedPurchaseInvoices = clbxPurchaseInvoices.CheckedItems.Cast<PurchaseInvoice>().ToArray();
            this.DialogResult = DialogResult.OK;
        }
    }
}
