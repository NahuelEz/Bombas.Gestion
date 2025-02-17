using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SI_SaleInvoice : Form
    {
        private int? SaleInvoiceID = null;
        private int? PreselectedSaleID = null;
        private int? DeliveryNoteID = null;
        private SaleInvoice CurrentSaleInvoice = null;
        private BindingList<Sale> AssociatedSales = new BindingList<Sale>();
        private bool SafeExit = false;
        
        public SI_SaleInvoice()
        {
            InitializeComponent();
            dgvSales.AutoGenerateColumns = false;
        }
        public SI_SaleInvoice(int parameter, SIParameterType parameterType)
        {
            switch (parameterType)
            {
                case SIParameterType.SaleInvoiceID:
                    {
                        SaleInvoiceID = parameter;
                        break;
                    }
                case SIParameterType.PreselectedSaleID:
                    {
                        PreselectedSaleID = parameter;
                        break;
                    }
                case SIParameterType.DeliveryNoteID:
                    {
                        DeliveryNoteID = parameter;
                        break;
                    }
            }
            InitializeComponent();
            dgvSales.AutoGenerateColumns = false;
        }

        private async void SI_SaleInvoice_Load(object sender, EventArgs e)
        {
            if (SaleInvoiceID.HasValue)
            {
                try
                {
                    CurrentSaleInvoice = await Task.Run(() => SaleInvoice.GetInvoiceById(SaleInvoiceID.Value));
                    AssociatedSales = await Task.Run(() => Sale.GetSalesBySaleInvoiceId(SaleInvoiceID.Value).ToBindingList());
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboInvoiceType.DataSource = new string[] { "A", "B", "C" };
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint SI201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SI201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando factura: " + CurrentSaleInvoice.SaleInvoiceID.ToString("D8");
                txtSaleInvoiceID.Text = CurrentSaleInvoice.SaleInvoiceID.ToString("D8");
                cboBusiness.SelectedValue = CurrentSaleInvoice.BusinessID;
                dtpInvoiceDate.Value = CurrentSaleInvoice.InvoiceDate;
                cboCustomer.SelectedValue = CurrentSaleInvoice.CustomerID;
                dgvSales.DataSource = AssociatedSales;
                cboInvoiceType.SelectedItem = CurrentSaleInvoice.InvoiceType;
                txtInvoiceNumber.Text = CurrentSaleInvoice.InvoiceNumber;
                nudTotalAmount.Value = CurrentSaleInvoice.TotalAmount;
                cboCurrency.SelectedValue = CurrentSaleInvoice.CurrencyID;
                cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            }
            else
            {
                try
                {
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    if (((List<Customer>)cboCustomer.DataSource).Count == 0)
                    {
                        MessageBox.Show("No hay clientes registrados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    // Waypoint SI202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SI202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                // Verifica si se solicitó preselección de venta o factura a partir de remito.
                if (PreselectedSaleID.HasValue)
                {
                    Sale preselectedSale;
                    List<SaleItem> saleItems;
                    try
                    {
                        preselectedSale = await Task.Run(() => Sale.GetSaleById(PreselectedSaleID.Value));
                        saleItems = await Task.Run(() => SaleItem.GetItemsBySaleId(PreselectedSaleID.Value));
                    }
                    catch (Exception dbException)
                    {
                        // Waypoint SI203
                        MessageBox.Show("Error en servidor MySQL."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.AppendLog("Exception at Waypoint SI203 (Flag: MySQL). Message: " + dbException.Message);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    // Calcula totales (para todos los ítems de la venta).
                    decimal totalAmount = saleItems.Sum(i => i.TotalAmount);
                    decimal totalBeforeTax = totalAmount * (100M - preselectedSale.Discount) / 100M;
                    decimal vatTotal = saleItems.Sum(i => (i.TotalAmount * i.VatPercentage / 100M)) * (100M - preselectedSale.Discount) / 100M;
                    decimal grandTotal = (totalBeforeTax + vatTotal);
                    // Setea valores en UI.
                    cboBusiness.SelectedValue = preselectedSale.BusinessID;
                    cboCustomer.SelectedValue = preselectedSale.CustomerID;
                    nudTotalAmount.Value = grandTotal;
                    cboCurrency.SelectedValue = preselectedSale.CurrencyID;
                    AssociatedSales.Add(preselectedSale);
                    dgvSales.DataSource = AssociatedSales;
                    cboBusiness.Enabled = false;
                    cboCustomer.Enabled = false;
                    cmsSaleOptions.Enabled = false;
                    btnLinkSale.Enabled = false;
                }
                else if (DeliveryNoteID.HasValue)
                {
                    DeliveryNote deliveryNote;
                    List<SaleItem> deliveryNoteItems;
                    Sale sale;
                    try
                    {
                        deliveryNote = await Task.Run(() => DeliveryNote.GetDeliveryNoteById(DeliveryNoteID.Value));
                        deliveryNoteItems = await Task.Run(() => SaleItem.GetItemsByDeliveryNoteId(DeliveryNoteID.Value));
                        sale = await Task.Run(() => Sale.GetSaleById(deliveryNote.SaleID));
                    }
                    catch (Exception dbException)
                    {
                        // Waypoint SI203
                        MessageBox.Show("Error en servidor MySQL."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.AppendLog("Exception at Waypoint SI203 (Flag: MySQL). Message: " + dbException.Message);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    // Calcula totales (solo para los ítems asociados al remito).
                    decimal totalAmount = deliveryNoteItems.Sum(i => i.TotalAmount);
                    decimal totalBeforeTax = totalAmount * (100M - sale.Discount) / 100M;
                    decimal vatTotal = deliveryNoteItems.Sum(i => (i.TotalAmount * i.VatPercentage / 100M)) * (100M - sale.Discount) / 100M;
                    decimal grandTotal = (totalBeforeTax + vatTotal);
                    // Setea valores en UI.
                    cboBusiness.SelectedValue = sale.BusinessID;
                    cboCustomer.SelectedValue = sale.CustomerID;
                    if (!string.IsNullOrWhiteSpace(deliveryNote.MaskedInvoiceNumber))
                    {
                        cboInvoiceType.SelectedItem = deliveryNote.InvoiceType;
                        txtInvoiceNumber.Text = deliveryNote.MaskedInvoiceNumber;
                        cboInvoiceType.Enabled = false;
                        txtInvoiceNumber.Enabled = false;
                    }
                    nudTotalAmount.Value = grandTotal;
                    cboCurrency.SelectedValue = sale.CurrencyID;
                    AssociatedSales.Add(sale);
                    dgvSales.DataSource = AssociatedSales;
                    cboBusiness.Enabled = false;
                    cboCustomer.Enabled = false;
                    cmsSaleOptions.Enabled = false;
                    btnLinkSale.Enabled = false;
                }
                else
                {
                    dgvSales.DataSource = AssociatedSales;
                    cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
                }
            }
        }
        private void SI_SaleInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentSaleInvoice == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }
        
        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboBusiness.SelectedValue = ((Customer)cboCustomer.SelectedItem).BusinessID;
            AssociatedSales.Clear();
        }

        private void btnLinkSale_Click(object sender, EventArgs e)
        {
            using (var form = new SI_SaleInvoice_Sale((int)cboCustomer.SelectedValue, AssociatedSales))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var selectedSales = form.SelectedSales;
                    foreach (var sale in selectedSales)
                    {
                        AssociatedSales.Add(sale);
                    }
                }
            }
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                MessageBox.Show("El número de factura está incompleto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudTotalAmount.Value == 0)
            {
                MessageBox.Show("El importe debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Comprueba duplicados (si es factura nueva)
            if (CurrentSaleInvoice == null)
            {
                string invoiceNumber = txtInvoiceNumber.Text.Trim();
                bool hasDuplicates;
                try
                {
                    hasDuplicates = await Task.Run(() => SaleInvoice.CheckInvoiceNumberDuplicates(invoiceNumber));
                }
                catch (Exception dbException)
                {
                    // Waypoint SI204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SI204 (Flag: MySQL). Message: " + dbException.Message);
                    return;
                }
                if (hasDuplicates)
                {
                    var prompt = MessageBox.Show("Ya existe otra factura registrada con el mismo número. ¿Desea continuar?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (prompt != DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            var invoice = new SaleInvoice()
            {
                SaleInvoiceID = (CurrentSaleInvoice == null) ? 0 : CurrentSaleInvoice.SaleInvoiceID,
                BusinessID = (int)cboBusiness.SelectedValue,
                InvoiceDate = dtpInvoiceDate.Value.Date,
                CustomerID = (int)cboCustomer.SelectedValue,
                InvoiceType = (string)cboInvoiceType.SelectedItem,
                InvoiceNumber = txtInvoiceNumber.Text,
                TotalAmount = nudTotalAmount.Value,
                CurrencyID = (int)cboCurrency.SelectedValue,
                Status = "Pendiente"
            };
            if (CurrentSaleInvoice == null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var handler = new DbTransactionHandler())
                        {
                            // Registro factura.
                            invoice.Insert(handler);
                            // "linkeo" la factura a las ventas asociadas, y actualizo el estado de las mismas.
                            foreach (var sale in AssociatedSales)
                            {
                                Sale.LinkInvoice(sale.SaleID, invoice.SaleInvoiceID, handler);
                                // Actualiza orden de reparación (si corresponde)
                                RepairOrder.UpdateInvoiceNumberBySaleId(sale.SaleID, invoice.InvoiceNumber, handler);
                            }
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Factura registrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint SI205
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SI205 (Flag: MySQL). Message: " + dbException.Message);
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
                            // Actualizo factura.
                            invoice.Update(handler);
                            // "des-linkeo" la factura de todas las ventas anteriores.
                            Sale.UnlinkInvoiceFromAll(invoice.SaleInvoiceID, handler);
                            // "linkeo" la factura a las ventas asociadas, y actualizo el estado de las mismas.
                            foreach (var sale in AssociatedSales)
                            {
                                Sale.LinkInvoice(sale.SaleID, invoice.SaleInvoiceID, handler);
                                // Actualiza orden de reparación (si corresponde)
                                RepairOrder.UpdateInvoiceNumberBySaleId(sale.SaleID, invoice.InvoiceNumber, handler);
                            }
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Factura actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint SI206
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SI206 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }

        private void cmsItemOpenSale_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            using (var form = new SA_Sale(selectedSale.SaleID, SAParameterType.SaleID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemUnlinkSale_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            AssociatedSales.RemoveAt(dgvSales.SelectedRows[0].Index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Verifica si se ha cargado una factura.
            if (CurrentSaleInvoice == null)
            {
                MessageBox.Show("No hay una factura cargada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtiene los datos de la factura.
            string invoiceNumber = txtInvoiceNumber.Text;
            decimal totalAmount = nudTotalAmount.Value;
            string customerName = ((Customer)cboCustomer.SelectedItem).CustomerName;

            // Crea el contenido del correo.
            string subject = "Recordatorio de Pago - Factura #" + invoiceNumber;
            string body = $"Estimado {customerName},\n\n" +
                           $"Le recordamos que tiene una deuda pendiente con nosotros por la cantidad de {totalAmount:C}.\n\n" +
                           "Por favor, realice el pago a la brevedad para evitar cargos adicionales.\n\n" +
                           "Gracias por su atención.";

            // Codifica el asunto y el cuerpo del mensaje para la URL.
            string encodedSubject = Uri.EscapeDataString(subject);
            string encodedBody = Uri.EscapeDataString(body);

            // Construye la URL del correo.
            string mailtoUrl = $"mailto:?subject={encodedSubject}&body={encodedBody}";

            // Abre la URL en el navegador predeterminado.
            try
            {
                System.Diagnostics.Process.Start(mailtoUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el correo electrónico: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public enum SIParameterType { SaleInvoiceID, PreselectedSaleID, DeliveryNoteID }
}
