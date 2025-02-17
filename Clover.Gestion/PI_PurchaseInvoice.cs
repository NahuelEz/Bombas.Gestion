using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PI_PurchaseInvoice : Form
    {
        private int? PurchaseInvoiceID = null;
        private PurchaseInvoice CurrentPurchaseInvoice = null;
        private BindingList<PurchaseOrder> PurchaseOrders = new BindingList<PurchaseOrder>();
        private bool SafeExit = false;
        
        public PI_PurchaseInvoice(int? PurchaseInvoiceID = null)
        {
            this.PurchaseInvoiceID = PurchaseInvoiceID;
            InitializeComponent();
            dgvPurchaseOrders.AutoGenerateColumns = false;
        }

        private async void PI_PurchaseInvoice_Load(object sender, EventArgs e)
        {
            if (PurchaseInvoiceID.HasValue)
            {
                try
                {
                    CurrentPurchaseInvoice = await Task.Run(() => PurchaseInvoice.GetInvoiceById(PurchaseInvoiceID.Value));
                    PurchaseOrders = await Task.Run(() => PurchaseOrder.GetPurchaseOrdersByPurchaseInvoiceId(PurchaseInvoiceID.Value).ToBindingList());
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboProvider.DisplayMember = "ProviderName";
                    cboProvider.ValueMember = "ProviderID";
                    cboProvider.DataSource = await Task.Run(() => Provider.GetProvidersLight());
                    cboInvoiceType.DataSource = new string[] { "A", "B", "C" };
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint PI201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PI201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando factura de proveedor: " + CurrentPurchaseInvoice.PurchaseInvoiceID.ToString("D8");
                txtPurchaseInvoiceID.Text = CurrentPurchaseInvoice.PurchaseInvoiceID.ToString("D8");
                cboBusiness.SelectedValue = CurrentPurchaseInvoice.BusinessID;
                dtpInvoiceDate.Value = CurrentPurchaseInvoice.InvoiceDate;
                cboProvider.SelectedValue = CurrentPurchaseInvoice.ProviderID;
                dgvPurchaseOrders.DataSource = PurchaseOrders;
                cboInvoiceType.SelectedItem = CurrentPurchaseInvoice.InvoiceType;
                txtInvoiceNumber.Text = CurrentPurchaseInvoice.InvoiceNumber;
                nudTotalAmount.Value = CurrentPurchaseInvoice.TotalAmount;
                cboCurrency.SelectedValue = CurrentPurchaseInvoice.CurrencyID;
                cboProvider.Enabled = false;

                // Si la factura ya tiene una orden de pago registrada, no se debe poder modificar.
                if (CurrentPurchaseInvoice.PayOrderID.HasValue)
                {
                    cboBusiness.Enabled = false;
                    dtpInvoiceDate.Enabled = false;
                    cmsItemUnlinkPurchaseOrder.Enabled = false;
                    btnLinkPurchaseOrder.Enabled = false;
                    cboInvoiceType.Enabled = false;
                    txtInvoiceNumber.ReadOnly = true;
                    nudTotalAmount.Enabled = false;
                    cboCurrency.Enabled = false;
                    btnAccept.Enabled = false;
                }
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
                    cboInvoiceType.DataSource = new string[] { "A", "B", "C" };
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint PI203
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PI203 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                dgvPurchaseOrders.DataSource = PurchaseOrders;
                cboProvider.SelectedIndexChanged += cboProvider_SelectedIndexChanged;
            }
        }
        private void PI_PurchaseInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentPurchaseInvoice == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica que haya un proveedor seleccionado.
            if (cboProvider.SelectedItem == null)
            {
                return;
            }

            cboBusiness.SelectedValue = ((Provider)cboProvider.SelectedItem).BusinessID;
            PurchaseOrders.Clear();
        }
        private void cboProvider_Validating(object sender, CancelEventArgs e)
        {
            // Borra el texto ingresado si no es un elemento de la lista.
            if (cboProvider.SelectedItem == null)
            {
                cboProvider.Text = string.Empty;
                PurchaseOrders.Clear();
            }
        }

        private void btnLinkPurchaseOrder_Click(object sender, EventArgs e)
        {
            // Verifica que haya un proveedor seleccionado.
            if (cboProvider.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new PI_PurchaseInvoice_PurchaseOrder((int)cboProvider.SelectedValue, PurchaseInvoiceID, PurchaseOrders))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var selectedPurchaseOrders = form.SelectedPurchaseOrders;
                    foreach (var order in selectedPurchaseOrders)
                    {
                        PurchaseOrders.Add(order);
                    }
                }
            }
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Verifica que haya un proveedor seleccionado.
            if (cboProvider.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica que el número de factura no este en blanco.
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                MessageBox.Show("El número de factura está incompleto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica que el importe sea mayor a 0.
            if (nudTotalAmount.Value == 0)
            {
                MessageBox.Show("El importe debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Comprueba duplicados (si es factura nueva).
            if (CurrentPurchaseInvoice == null)
            {
                bool hasDuplicates;
                string invoiceNumber = txtInvoiceNumber.Text.Trim();
                try
                {
                    hasDuplicates = await Task.Run(() => PurchaseInvoice.CheckInvoiceNumberDuplicates(invoiceNumber));
                }
                catch (Exception dbException)
                {
                    // Waypoint PI204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PI204 (Flag: MySQL). Message: " + dbException.Message);
                    return;
                }
                if (hasDuplicates)
                {
                    var dialog = MessageBox.Show("Ya existe otra factura registrada con el mismo número. ¿Desea continuar?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialog != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            // Construye objeto "purchase_invoice".
            var invoice = new PurchaseInvoice()
            {
                PurchaseInvoiceID = (CurrentPurchaseInvoice == null) ? 0 : CurrentPurchaseInvoice.PurchaseInvoiceID,
                BusinessID = (int)cboBusiness.SelectedValue,
                InvoiceDate = dtpInvoiceDate.Value.Date,
                ProviderID = (int)cboProvider.SelectedValue,
                InvoiceType = (string)cboInvoiceType.SelectedItem,
                InvoiceNumber = txtInvoiceNumber.Text,
                TotalAmount = nudTotalAmount.Value,
                CurrencyID = (int)cboCurrency.SelectedValue,
                Status = "Pendiente"
            };
            if (CurrentPurchaseInvoice == null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var handler = new DbTransactionHandler())
                        {
                            invoice.Insert(handler);
                            foreach (var order in PurchaseOrders)
                            {
                                PurchaseOrder.LinkPurchaseInvoice(order.PurchaseOrderID, invoice.PurchaseInvoiceID, handler);
                            }
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Factura de proveedor registrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint PI205
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PI205 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var handler = new DbTransactionHandler())
                        {
                            invoice.Update(handler);
                            PurchaseOrder.UnlinkPurchaseInvoice(invoice.PurchaseInvoiceID);
                            foreach (var order in PurchaseOrders)
                            {
                                PurchaseOrder.LinkPurchaseInvoice(order.PurchaseOrderID, invoice.PurchaseInvoiceID, handler);
                            }
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Factura de proveedor actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint PI206
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PI206 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }

        private void cmsItemOpenPurchaseOrder_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedPurchaseOrder = (PurchaseOrder)dgvPurchaseOrders.SelectedRows[0].DataBoundItem;
            using (var form = new PO_PurchaseOrder(selectedPurchaseOrder.PurchaseOrderID, POParameterType.PurchaseOrderID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemUnlinkPurchaseOrder_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                return;
            }
            PurchaseOrders.RemoveAt(dgvPurchaseOrders.SelectedRows[0].Index);
        }

        
    }
}
