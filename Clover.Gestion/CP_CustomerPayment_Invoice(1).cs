using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CP_CustomerPayment_Invoice : Form
    {
        public List<SaleInvoice> SelectedInvoices = new List<SaleInvoice>();

        private int CustomerID;
        private IList<SaleInvoice> CurrentInvoices;
        
        public CP_CustomerPayment_Invoice(int CustomerID, IList<SaleInvoice> CurrentInvoices)
        {
            this.CustomerID = CustomerID;
            this.CurrentInvoices = CurrentInvoices;
            InitializeComponent();
        }

        private async void CP_CustomerPayment_Invoice_Load(object sender, EventArgs e)
        {
            List<SaleInvoice> invoicesFromCustomer;
            try
            {
                invoicesFromCustomer = await Task.Run(() => SaleInvoice.GetInvoicesByCustomerId(CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint CP301
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CP301 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            var unselectedInvoices = invoicesFromCustomer.Where(i => !CurrentInvoices.Any(x => x.SaleInvoiceID == i.SaleInvoiceID)).ToList();
            clbxAssociatedInvoices.DataSource = unselectedInvoices;
        }

        private void clbxAssociatedInvoices_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((SaleInvoice)e.ListItem).InvoiceNumber;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (clbxAssociatedInvoices.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedInvoices = clbxAssociatedInvoices.CheckedItems.Cast<SaleInvoice>().ToList();
            this.DialogResult = DialogResult.OK;
        }
    }
}
