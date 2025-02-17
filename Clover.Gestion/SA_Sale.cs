using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_Sale : Form
    {
        private int? SaleID = null;
        private int? PreselectedEstimateID = null;
        private Sale CurrentSale = null;
        private List<SaleItem> Items = new List<SaleItem>();
        private bool SafeExit = false;

        private string SaleNotes = null;

        public SA_Sale()
        {
            InitializeComponent();
        }
        public SA_Sale(int parameter, SAParameterType parameterType)
        {
            switch (parameterType)
            {
                case SAParameterType.SaleID:
                    {
                        SaleID = parameter;
                        break;
                    }
                case SAParameterType.PreselectedEstimateID:
                    {
                        PreselectedEstimateID = parameter;
                        break;
                    }
            }
            InitializeComponent();
        }

        private async void SA_Sale_Load(object sender, EventArgs e)
        {
            // Ajusta formulario en pantalla
            int availableHeight = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Height > availableHeight)
            {
                this.Height = availableHeight;
                this.CenterToScreen();
            }
            if (SaleID.HasValue)
            {
                // Desactiva eventos
                cboCustomer.SelectedIndexChanged -= cboCustomer_SelectedIndexChanged;
                cboEstimate.SelectedIndexChanged -= cboEstimate_SelectedIndexChanged;
                chkSaleWithoutEstimate.CheckedChanged -= chkSaleWithoutEstimate_CheckedChanged;
                nudDiscount.ValueChanged -= nudDiscount_ValueChanged;
                try
                {
                    CurrentSale = await Task.Run(() => Sale.GetSaleById(SaleID.Value));
                    Items = await Task.Run(() => SaleItem.GetItemsBySaleId(SaleID.Value));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboEstimate.ValueMember = "EstimateID";
                    cboEstimate.DataSource = await Task.Run(() => Estimate.GetEstimatesLight());
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                    cboPayment.DisplayMember = "PaymentName";
                    cboPayment.ValueMember = "PaymentID";
                    cboPayment.DataSource = await Task.Run(() => Payment.GetPayments());
                    cboShipping.DisplayMember = "ShippingName";
                    cboShipping.ValueMember = "ShippingID";
                    cboShipping.DataSource = await Task.Run(() => Shipping.GetShippings());
                    cboDepartment.DisplayMember = "DepartmentName";
                    cboDepartment.ValueMember = "DepartmentID";
                    cboDepartment.DataSource = await Task.Run(() => Department.GetDepartments());
                }
                catch (Exception dbException)
                {
                    // Waypoint SA201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SA201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = $"Visualizando venta: {CurrentSale.SaleID:D8}";
                txtSaleID.Text = CurrentSale.SaleID.ToString("D8");
                dtpDate.Enabled = false;
                dtpDate.Value = CurrentSale.Date;
                cboBusiness.Enabled = false;
                cboBusiness.SelectedValue = CurrentSale.BusinessID;
                cboDepartment.Enabled = false;
                cboDepartment.SelectedValue = CurrentSale.DepartmentID;
                cboCustomer.Enabled = false;
                cboCustomer.SelectedValue = CurrentSale.CustomerID;
                if (CurrentSale.EstimateID.HasValue)
                {
                    chkSaleWithoutEstimate.Enabled = false;
                    chkSaleWithoutEstimate.Checked = false;
                    cboEstimate.Enabled = false;
                    cboEstimate.SelectedValue = CurrentSale.EstimateID.Value;
                }
                else
                {
                    chkSaleWithoutEstimate.Enabled = false;
                    chkSaleWithoutEstimate.Checked = true;
                    cboEstimate.Enabled = false;
                    cboEstimate.SelectedItem = null;
                }
                pnlCondition.Enabled = false;
                rbnIsWhite.Checked = !CurrentSale.IsUnmarked;
                rbnIsBlack.Checked = CurrentSale.IsUnmarked;
                nudDiscount.Enabled = false;
                nudDiscount.Value = CurrentSale.Discount;
                cboCurrency.Enabled = false;
                cboCurrency.SelectedValue = CurrentSale.CurrencyID;
                btnCurrencyConverter.Enabled = false;
                cboPayment.SelectedValue = CurrentSale.PaymentID;
                dtpDeliveryDate.Value = CurrentSale.DeliveryDate;
                cboShipping.SelectedValue = CurrentSale.ShippingID;
                if (!CurrentSale.IsUnmarked)
                {
                    btnManageDeliveryNotes.Enabled = true;
                    btnManageInvoices.Enabled = true;
                }
                txtPurchaseOrderNumber.Text = CurrentSale.PurchaseOrderNumber;
                SaleNotes = CurrentSale.Notes;
                btnAccept.Text = "Guardar";
                ComputeTotals();
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
                    cboEstimate.ValueMember = "EstimateID";
                    cboEstimate.DataSource = await Task.Run(() => Estimate.GetAvailableEstimates());
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                    cboPayment.DisplayMember = "PaymentName";
                    cboPayment.ValueMember = "PaymentID";
                    cboPayment.DataSource = await Task.Run(() => Payment.GetPayments());
                    cboShipping.DisplayMember = "ShippingName";
                    cboShipping.ValueMember = "ShippingID";
                    cboShipping.DataSource = await Task.Run(() => Shipping.GetShippings());
                    cboDepartment.DisplayMember = "DepartmentName";
                    cboDepartment.ValueMember = "DepartmentID";
                    cboDepartment.DataSource = await Task.Run(() => Department.GetDepartments());
                }
                catch (Exception dbException)
                {
                    // Waypoint SA202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SA202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                // Verifica si hay presupuesto preseleccionado.
                if (PreselectedEstimateID.HasValue)
                {
                    if (((List<Estimate>)cboEstimate.DataSource).Count(x => x.EstimateID == PreselectedEstimateID.Value) != 0)
                    {
                        cboEstimate.SelectedValue = PreselectedEstimateID.Value;
                    }
                    else
                    {
                        MessageBox.Show("El presupuesto seleccionado no está disponible para registrar una venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                // Selecciona "venta sin presupuesto" si no hay presupuestos.
                if (((List<Estimate>)cboEstimate.DataSource).Count == 0)
                {
                    chkSaleWithoutEstimate.Checked = true;
                    chkSaleWithoutEstimate.Enabled = false;
                }
            }
        }
        private void SA_Sale_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentSale == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            string selectedCurrency = (CurrentSale == null) ? ((Currency)cboCurrency.SelectedItem).CurrencySymbol : CurrentSale.CurrencySymbol;
            using (var form = new SA_Items(Items, selectedCurrency, (CurrentSale != null)))
            {
                form.ShowDialog();
            }
            ComputeTotals();
        }
        private void btnEditNotes_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_TextBox(SaleNotes, 512))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaleNotes = form.EditedText;
                }
            }
        }
        private void btnCurrencyConverter_Click(object sender, EventArgs e)
        {
            if (Items.Count == 0)
            {
                MessageBox.Show("La venta no tiene ítems.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var form = new SA_CurrencyConverter((Currency)cboCurrency.SelectedItem))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    cboCurrency.SelectedValue = form.DestinationCurrencyID;
                    foreach (var item in Items)
                    {
                        item.Amount = item.Amount * form.ExchangeRate;
                        item.TotalAmount = item.Quantity * item.Amount;
                        if (item.Cost.HasValue)
                        {
                            item.Cost = item.Cost.Value * form.ExchangeRate;
                        }
                    }
                    ComputeTotals();
                }
            }
        }
        private async void btnManageDeliveryNotes_Click(object sender, EventArgs e)
        {
            using (var form = new SA_DeliveryNoteManager(CurrentSale.SaleID))
            {
                form.ShowDialog();
            }
            // Actualiza los ítems de la venta por si se eliminaron remitos.
            try
            {
                Items = await Task.Run(() => SaleItem.GetItemsBySaleId(SaleID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint SA203
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA203 (Flag: MySQL). Message: " + dbException.Message);
            }
        }
        private void btnManageInvoices_Click(object sender, EventArgs e)
        {
            using (var form = new SA_InvoiceManager(CurrentSale.SaleID))
            {
                form.ShowDialog();
            }
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (Items.Count == 0)
            {
                MessageBox.Show("La venta debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Any(i => i.TotalAmount == 0))
            {
                MessageBox.Show("El precio de cada uno de los ítems debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtpDeliveryDate.Value.Date < dtpDate.Value.Date)
            {
                MessageBox.Show("La fecha de entrega debe ser posterior o igual a la fecha de venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!SaleID.HasValue && (int)cboDepartment.SelectedValue == 2 && ((Currency)cboCurrency.SelectedItem).CurrencySymbol != "ARS")
            {
                MessageBox.Show("Las ventas del departamento Venta directa de bombas solo pueden registrarse en pesos argentinos. Utilice el convertidor de moneda.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var sale = new Sale()
            {
                SaleID = (CurrentSale == null) ? 0 : CurrentSale.SaleID,
                BusinessID = (int)cboBusiness.SelectedValue,
                CustomerID = (int)cboCustomer.SelectedValue,
                EstimateID = (chkSaleWithoutEstimate.Checked) ? new int?() : (int)cboEstimate.SelectedValue,
                Date = dtpDate.Value.Date,
                IsUnmarked = rbnIsBlack.Checked,
                HasInvoices = (CurrentSale == null) ? false : CurrentSale.HasInvoices,
                HasPayments = (CurrentSale == null) ? false : CurrentSale.HasPayments,
                Discount = nudDiscount.Value,
                TotalBeforeTax = (Items.Sum(i => i.TotalAmount) * (100M - nudDiscount.Value) / 100M),
                CurrencyID = (int)cboCurrency.SelectedValue,
                PaymentID = (int)cboPayment.SelectedValue,
                DeliveryDate = dtpDeliveryDate.Value.Date,
                ShippingID = (int)cboShipping.SelectedValue,
                Shipped = (CurrentSale == null) ? false : CurrentSale.Shipped,
                ShippingCarrierID = (CurrentSale == null) ? new int?() : CurrentSale.ShippingCarrierID,
                ShippingDate = (CurrentSale == null) ? new DateTime?() : CurrentSale.ShippingDate,
                PurchaseOrderNumber = txtPurchaseOrderNumber.Text.NullIfEmpty(),
                Notes = SaleNotes.NullIfEmpty(),
                DepartmentID = (int)cboDepartment.SelectedValue,
                TotalCost = Items.All(i => i.Cost.HasValue) ? Items.Sum(i => i.Quantity * i.Cost.Value) : new decimal?()
            };
            if (CurrentSale == null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var handler = new DbTransactionHandler())
                        {
                            // Registra nueva venta.
                            sale.Insert(handler);
                            // Registra ítems.
                            int itemNumber = 1;
                            foreach (var item in Items)
                            {
                                item.SaleID = sale.SaleID;
                                item.ItemNumber = itemNumber;
                                item.Insert(handler);
                            }
                            // Actualiza presupuesto (si corresponde)
                            if (sale.EstimateID.HasValue)
                            {
                                Estimate.UpdateStatus(sale.EstimateID.Value, "Vendido", handler);
                            }
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Venta registrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint SA205
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SA205 (Flag: MySQL). Message: " + dbException.Message);
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
                            // Actualiza venta.
                            sale.Update(handler);
                            // Elimina ítems asociados.
                            SaleItem.DeleteItemsBySaleId(sale.SaleID, handler);
                            // Registra items actualizados.
                            int itemNumber = 1;
                            foreach (var item in Items)
                            {
                                item.SaleID = sale.SaleID;
                                item.ItemNumber = itemNumber;
                                item.Insert(handler);
                            }
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Venta actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint SA206
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SA206 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedItem != null && chkSaleWithoutEstimate.Checked)
            {
                cboBusiness.SelectedValue = ((Customer)cboCustomer.SelectedItem).BusinessID;
            }
        }
        private void cboEstimate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstimate.SelectedItem == null)
            {
                return;
            }
            // Carga información del presupuesto seleccionado.
            int selectedEstimateId = (int)cboEstimate.SelectedValue;
            Estimate estimate = null;
            try
            {
                estimate = Estimate.GetEstimateById(selectedEstimateId);
                Items = SaleItem.GetItemsFromEstimate(selectedEstimateId);
            }
            catch (Exception dbException)
            {
                // Waypoint SA207
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA207 (Flag: MySQL). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
                return;
            }
            // Aplica descuento sobre ítems.
            if (estimate.Discount != 0)
            {
                foreach (var item in Items)
                {
                    item.Amount = item.Amount * (1 - estimate.Discount / 100);
                    item.TotalAmount = item.Quantity * item.Amount;
                }
            }
            cboBusiness.SelectedValue = estimate.BusinessID;
            cboDepartment.SelectedValue = estimate.DepartmentID;
            cboCustomer.SelectedValue = estimate.CustomerID;
            rbnIsWhite.Checked = !estimate.IsUnmarked;
            rbnIsBlack.Checked = estimate.IsUnmarked;
            nudDiscount.Value = 0;
            cboCurrency.SelectedValue = estimate.CurrencyID;
            cboPayment.SelectedValue = estimate.PaymentID;
            if (estimate.DeliveryDelay.HasValue)
            {
                dtpDeliveryDate.Value = dtpDate.Value.AddDays(estimate.DeliveryDelay.Value);
            }
            ComputeTotals();
        }
        private void cboEstimate_Format(object sender, ListControlConvertEventArgs e)
        {
            var estimate = (Estimate)e.ListItem;
            e.Value = $"{estimate.EstimateID} - {estimate.CustomerName}";
        }
        private void chkSaleWithoutEstimate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSaleWithoutEstimate.Checked)
            {
                cboBusiness.Enabled = true;
                cboDepartment.Enabled = true;
                cboCustomer.Enabled = true;
                cboEstimate.Enabled = false;
                cboEstimate.SelectedItem = null;
                pnlCondition.Enabled = true;
                Items.Clear();
                ComputeTotals();
            }
            else
            {
                cboBusiness.Enabled = false;
                cboDepartment.Enabled = false;
                cboCustomer.Enabled = false;
                cboEstimate.Enabled = true;
                cboEstimate.SelectedIndex = 0;
                pnlCondition.Enabled = false;
            }
        }
        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            ComputeTotals();
        }

        private void ComputeTotals()
        {
            lblItemsCount.Text = $"{Items.Count} ítem(s)";
            decimal totalAmount = Items.Sum(i => i.TotalAmount);
            decimal totalBeforeTax = (totalAmount * (100M - nudDiscount.Value) / 100M);
            decimal vatTotal = (Items.Sum(i => (i.TotalAmount * i.VatPercentage / 100M)) * (100M - nudDiscount.Value) / 100M);
            decimal grandTotal = (totalBeforeTax + vatTotal);
            txtTotalAmount.Text = totalAmount.ToString("N2");
            txtTotalBeforeTax.Text = totalBeforeTax.ToString("N2");
            txtVatTotal.Text = vatTotal.ToString("N2");
            txtGrandTotal.Text = grandTotal.ToString("N2");
        }
    }

    public enum SAParameterType { SaleID, PreselectedEstimateID }
}