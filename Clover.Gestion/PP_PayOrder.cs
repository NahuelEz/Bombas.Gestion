using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PP_PayOrder : Form
    {
        private int? PayOrderID = null;
        private PayOrder CurrentPayOrder = null;
        public BindingList<PurchaseInvoice> PurchaseInvoices = new BindingList<PurchaseInvoice>();
        public BindingList<PayOrderPayment> Payments = new BindingList<PayOrderPayment>();
        private bool SafeExit = false;
        
        public PP_PayOrder(int? PayOrderID = null)
        {
            this.PayOrderID = PayOrderID;
            InitializeComponent();
            dgvPurchaseInvoices.AutoGenerateColumns = false;
            dgvPayments.AutoGenerateColumns = false;
        }

        private async void PP_PayOrder_Load(object sender, EventArgs e)
        {
            if (PayOrderID.HasValue)
            {
                try
                {
                    CurrentPayOrder = await Task.Run(() => PayOrder.GetPayOrderById(PayOrderID.Value));
                    PurchaseInvoices = await Task.Run(() => PurchaseInvoice.GetInvoicesByPayOrderId(PayOrderID.Value).ToBindingList());
                    Payments = await Task.Run(() => PayOrderPayment.GetPaymentsByPayOrderId(PayOrderID.Value).ToBindingList());
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboProvider.DisplayMember = "ProviderName";
                    cboProvider.ValueMember = "ProviderID";
                    cboProvider.DataSource = await Task.Run(() => Provider.GetProvidersLight());
                }
                catch (Exception dbException)
                {
                    // Waypoint PP201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PP201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando orden de pago: " + CurrentPayOrder.PayOrderID.ToString("D8");
                txtPayOrderID.Text = CurrentPayOrder.PayOrderID.ToString("D8");
                cboBusiness.SelectedValue = CurrentPayOrder.BusinessID;
                dtpDate.Value = CurrentPayOrder.Date;
                cboProvider.SelectedValue = CurrentPayOrder.ProviderID;
                dgvPurchaseInvoices.DataSource = PurchaseInvoices;
                dgvPayments.DataSource = Payments;
                cboProvider.SelectedIndexChanged += cboProvider_SelectedIndexChanged;
            }
            else
            {
                try
                {
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboProvider.DisplayMember = "ProviderName";
                    cboProvider.ValueMember = "ProviderID";
                    cboProvider.DataSource = await Task.Run(() => Provider.GetProvidersLight());
                    if (((List<Provider>)cboProvider.DataSource).Count == 0)
                    {
                        MessageBox.Show("No hay proveedores registrados en el sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                }
                catch (Exception dbException)
                {
                    // Waypoint PP202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PP202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                dgvPurchaseInvoices.DataSource = PurchaseInvoices;
                dgvPayments.DataSource = Payments;
                cboProvider.SelectedIndexChanged += cboProvider_SelectedIndexChanged;
            }
        }
        private void PP_PayOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentPayOrder == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }
        
        private void btnLinkInvoice_Click(object sender, EventArgs e)
        {
            using (var form = new PP_PayOrder_PurchaseInvoice((int)cboProvider.SelectedValue, PayOrderID, PurchaseInvoices))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var selectedPurchaseInvoices = form.SelectedPurchaseInvoices;
                    foreach (var order in selectedPurchaseInvoices)
                    {
                        PurchaseInvoices.Add(order);
                    }
                }
            }
        }
        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            using (var form = new PP_PayOrder_Payment())
            {
                form.ShowDialog(this);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (PurchaseInvoices.Count == 0)
            {
                MessageBox.Show("Debe asociar al menos una factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Payments.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un medio de pago.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda orden de pago.
            try
            {
                await SavePayOrderAsync();
                this.Text = "Visualizando orden de pago: " + CurrentPayOrder.PayOrderID.ToString("D8");
                txtPayOrderID.Text = CurrentPayOrder.PayOrderID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint PP203
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP203 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Provider selectedProvider = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentPayOrder.BusinessID));
                selectedProvider = await Task.Run(() => Provider.GetProviderById(CurrentPayOrder.ProviderID));
            }
            catch (Exception dbException)
            {
                // Waypoint PP204
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP204 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta presupuesto pdf.
            try
            {
                await ExportPdfAsync(CurrentPayOrder, PurchaseInvoices.ToList(), Payments.ToList(), selectedBusiness, selectedProvider);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PP205
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP205. Message: " + pdfExportException.Message);
                return;
            }
            MessageBox.Show("Orden de pago guardada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeExit = true;
            this.Close();
        }
        private async void btnMakePdf_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (PurchaseInvoices.Count == 0)
            {
                MessageBox.Show("Debe asociar al menos una factura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Payments.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un medio de pago.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda orden de pago.
            try
            {
                await SavePayOrderAsync();
                this.Text = "Visualizando orden de pago: " + CurrentPayOrder.PayOrderID.ToString("D8");
                txtPayOrderID.Text = CurrentPayOrder.PayOrderID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint PP206
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP206 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Provider selectedProvider = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentPayOrder.BusinessID));
                selectedProvider = await Task.Run(() => Provider.GetProviderById(CurrentPayOrder.ProviderID));
            }
            catch (Exception dbException)
            {
                // Waypoint PP207
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP207 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta presupuesto pdf.
            string pdfPath = string.Empty;
            try
            {
                pdfPath = await ExportPdfAsync(CurrentPayOrder, PurchaseInvoices.ToList(), Payments.ToList(), selectedBusiness, selectedProvider);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PP208
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP208. Message: " + pdfExportException.Message);
                return;
            }
            // Abre archivo pdf.
            try
            {
                using (var pdfOpenProcess = new System.Diagnostics.Process())
                {
                    pdfOpenProcess.StartInfo.FileName = pdfPath;
                    pdfOpenProcess.Start();
                }
            }
            catch (Exception pdfOpenException)
            {
                // Waypoint PP209
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP209. Message: " + pdfOpenException.Message);
            }
        }

        private void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboBusiness.SelectedValue = ((Provider)cboProvider.SelectedItem).BusinessID;
            PurchaseInvoices.Clear();
        }

        private void cmsItemOpenInvoice_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPurchaseInvoice = (PurchaseInvoice)dgvPurchaseInvoices.SelectedRows[0].DataBoundItem;
            using (var form = new PI_PurchaseInvoice(selectedPurchaseInvoice.PurchaseInvoiceID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemUnlinkInvoice_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            PurchaseInvoices.RemoveAt(dgvPurchaseInvoices.SelectedRows[0].Index);
        }
        private void cmsItemOpenPayment_Click(object sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPayment = (PayOrderPayment)dgvPayments.SelectedRows[0].DataBoundItem;
            using (var form = new PP_PayOrder_Payment(selectedPayment))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemDeletePayment_Click(object sender, EventArgs e)
        {
            if (dgvPayments.SelectedRows.Count == 0)
            {
                return;
            }
            Payments.RemoveAt(dgvPayments.SelectedRows[0].Index);
        }

        private async Task SavePayOrderAsync()
        {
            // Construye objeto "pay_order".
            var order = new PayOrder
            {
                PayOrderID = (CurrentPayOrder == null) ? 0 : CurrentPayOrder.PayOrderID,
                BusinessID = (int)cboBusiness.SelectedValue,
                ProviderID = (int)cboProvider.SelectedValue,
                Date = dtpDate.Value.Date
            };
            await Task.Run(() =>
            {
                using (var handler = new DbTransactionHandler())
                {
                    if (CurrentPayOrder == null)
                    {
                        // Registra nueva orden de pago.
                        order.Insert(handler);
                        // "linkeo" el pago a las facturas asociadas, y actualizo el estado de las mismas.
                        foreach (var invoice in PurchaseInvoices)
                        {
                            PurchaseInvoice.UpdateStatus(invoice.PurchaseInvoiceID, "Pago", handler);
                            PurchaseInvoice.LinkPayOrder(invoice.PurchaseInvoiceID, order.PayOrderID, handler);
                        }
                        // Registra pagos correspondientes a la orden.
                        foreach (var payment in Payments)
                        {
                            payment.PayOrderID = order.PayOrderID;
                            payment.Insert(handler);
                        }
                    }
                    else
                    {
                        // Actualiza orden de pago.
                        order.Update(handler);
                        // "des-linkeo" el pago de todas las facturas anteriores, y actualizo el estado de las mismas.
                        // ATENCIÓN: No se debe alterar el orden de estas operaciones.
                        PurchaseInvoice.UpdateStatusByPayOrderId(order.PayOrderID, "Pendiente", handler);
                        PurchaseInvoice.UnlinkPayOrder(order.PayOrderID, handler);
                        // "linkeo" el pago a las facturas asociadas, y actualizo el estado de las mismas.
                        foreach (var invoice in PurchaseInvoices)
                        {
                            PurchaseInvoice.UpdateStatus(invoice.PurchaseInvoiceID, "Pago", handler);
                            PurchaseInvoice.LinkPayOrder(invoice.PurchaseInvoiceID, order.PayOrderID, handler);
                        }
                        // Remueve todos los pagos asociados.
                        PayOrderPayment.DeleteByPayOrderId(order.PayOrderID, handler);
                        // Registra pagos correspondientes a la orden.
                        foreach (var payment in Payments)
                        {
                            payment.PayOrderID = order.PayOrderID;
                            payment.Insert(handler);
                        }

                    }
                    handler.CommitTransaction();
                }
            });
            CurrentPayOrder = order;
        }
        private async Task<string> ExportPdfAsync(PayOrder order, List<PurchaseInvoice> invoices, List<PayOrderPayment> payments, Business business, Provider provider)
        {
            // Comprobación carpeta de destino.
            string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Ordenes de pago " + business.BusinessName) :
                Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, "Ordenes de pago " + business.BusinessName);
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string pdfPath = Path.Combine(destinationFolder, $"Orden de pago {order.PayOrderID:D8} - {provider.ProviderName.RemoveIllegalCharacters()}.pdf");
            // Generación de orden de pago PDF.
            await Task.Run(() => PdfGeneration.ExportPdfPayOrder(order, invoices, payments, business, provider, pdfPath));
            return pdfPath;
        }
    }
}
