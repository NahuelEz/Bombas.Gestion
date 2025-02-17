using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CU_CustomerHistory : Form
    {
        private int CustomerID;
        private string CustomerName;

        public CU_CustomerHistory(int CustomerID, string CustomerName)
        {
            this.CustomerID = CustomerID;
            this.CustomerName = CustomerName;
            InitializeComponent();
            dgvHistory.AutoGenerateColumns = false;
        }

        private async void CU_CustomerHistory_Load(object sender, EventArgs e)
        {
            // Muestra ID y nombre de cliente
            lblCustomerName.Text = $"{CustomerID:D4} - {CustomerName}";
            // Recupera información para historial.
            List<Estimate> estimates;
            List<Sale> sales;
            List<SaleInvoice> invoices;
            List<CustomerPayment> payments;
            List<RepairOrder> orders;
            try
            {
                estimates = await Task.Run(() => Estimate.GetEstimatesByCustomerId(CustomerID));
                sales = await Task.Run(() => Sale.GetSalesByCustomerId(CustomerID));
                invoices = await Task.Run(() => SaleInvoice.GetInvoicesByCustomerId(CustomerID));
                payments = await Task.Run(() => CustomerPayment.GetPaymentsByCustomerId(CustomerID));
                orders = await Task.Run(() => RepairOrder.GetRepairOrdersByCustomerId(CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint CU301
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU301 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            // Concatena ambas secuencias.
            var dateSelector = new Func<DbEntity, DateTime>((param) =>
            {
                if (param is Estimate)
                {
                    return ((Estimate)param).Date;
                }
                else if (param is Sale)
                {
                    return ((Sale)param).Date;
                }
                else if (param is SaleInvoice)
                {
                    return ((SaleInvoice)param).InvoiceDate;
                }
                else if (param is CustomerPayment)
                {
                    return ((CustomerPayment)param).Date;
                }
                else
                {
                    return ((RepairOrder)param).Date;
                }
            });
            var records = (estimates.Cast<DbEntity>().Concat(
                sales.Cast<DbEntity>()).Concat(
                invoices.Cast<DbEntity>()).Concat(
                payments.Cast<DbEntity>()).Concat(
                orders.Cast<DbEntity>())).OrderByDescending(dateSelector).Take(300).ToList();
            // Carga información en interfaz.
            dgvHistory.DataSource = records;
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                object entity = dgvHistory.Rows[e.RowIndex].DataBoundItem;
                if (entity is Estimate)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((Estimate)entity).Date;
                            break;
                        case 1:
                            e.Value = ((Estimate)entity).BusinessName;
                            break;
                        case 2:
                            e.Value = "Presupuesto";
                            break;
                        case 3:
                            e.Value = ((Estimate)entity).EstimateID;
                            break;
                    }
                }
                else if (entity is Sale)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((Sale)entity).Date;
                            break;
                        case 1:
                            e.Value = ((Sale)entity).BusinessName;
                            break;
                        case 2:
                            e.Value = "Venta";
                            break;
                        case 3:
                            e.Value = ((Sale)entity).SaleID;
                            break;
                    }
                }
                else if (entity is SaleInvoice)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((SaleInvoice)entity).InvoiceDate;
                            break;
                        case 1:
                            e.Value = ((SaleInvoice)entity).BusinessName;
                            break;
                        case 2:
                            e.Value = "Factura";
                            break;
                        case 3:
                            e.Value = ((SaleInvoice)entity).SaleInvoiceID;
                            break;
                    }
                }
                else if (entity is CustomerPayment)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((CustomerPayment)entity).Date;
                            break;
                        case 1:
                            e.Value = ((CustomerPayment)entity).BusinessName;
                            break;
                        case 2:
                            e.Value = "Pago";
                            break;
                        case 3:
                            e.Value = ((CustomerPayment)entity).CustomerPaymentID;
                            break;
                    }
                }
                else
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((RepairOrder)entity).Date;
                            break;
                        case 1:
                            e.Value = string.Empty;
                            break;
                        case 2:
                            e.Value = "Orden de reparación";
                            break;
                        case 3:
                            e.Value = ((RepairOrder)entity).RepairOrderID;
                            break;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOpenSelected_Click(object sender, EventArgs e)
        {
            if (dgvHistory.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedItem = (DbEntity)dgvHistory.SelectedRows[0].DataBoundItem;
            if (selectedItem is Estimate)
            {
                using (var form = new ES_Estimate(selectedItem.PrimaryKeyID, ESParameterType.EstimateID))
                {
                    form.ShowDialog();
                }
            }
            else if (selectedItem is Sale)
            {
                using (var form = new SA_Sale(selectedItem.PrimaryKeyID, SAParameterType.SaleID))
                {
                    form.ShowDialog();
                }
            }
            else if (selectedItem is SaleInvoice)
            {
                using (var form = new SI_SaleInvoice(selectedItem.PrimaryKeyID, SIParameterType.SaleInvoiceID))
                {
                    form.ShowDialog();
                }
            }
            else if (selectedItem is CustomerPayment)
            {
                using (var form = new CP_CustomerPayment(selectedItem.PrimaryKeyID, CPParameterType.CustomerPaymentID))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                using (var form = new RO_RepairOrder(selectedItem.PrimaryKeyID))
                {
                    form.ShowDialog();
                }
            }
        }
    }
}
