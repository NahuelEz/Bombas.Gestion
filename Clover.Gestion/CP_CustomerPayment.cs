using Clover.DbLayer;
using Clover.Shared;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CP_CustomerPayment : Form
    {
        private int? CustomerPaymentID = null;
        private int? PreselectedSaleID = null;
        private CustomerPayment CurrentCustomerPayment = null;
        private BindingList<SaleInvoice> AssociatedInvoices = new BindingList<SaleInvoice>();
        private BindingList<Sale> AssociatedSales = new BindingList<Sale>();
        private bool SafeExit = false;



      


        public CP_CustomerPayment()
        {
            InitializeComponent();
            dgvAssociatedInvoices.AutoGenerateColumns = false;
            dgvAssociatedSales.AutoGenerateColumns = false;
        }
        public CP_CustomerPayment(int parameter, CPParameterType parameterType)
        {
            switch (parameterType)
            {
                case CPParameterType.CustomerPaymentID:
                    {
                        CustomerPaymentID = parameter;
                        break;
                    }
                case CPParameterType.PreselectedSaleID:
                    {
                        PreselectedSaleID = parameter;
                        break;
                    }
            }
            InitializeComponent();
            dgvAssociatedInvoices.AutoGenerateColumns = false;
            dgvAssociatedSales.AutoGenerateColumns = false;
        }

        private async void CP_CustomerPayment_Load(object sender, EventArgs e)
        {
            if (CustomerPaymentID.HasValue)
            {
                try
                {
                    CurrentCustomerPayment = await Task.Run(() => CustomerPayment.GetPaymentById(CustomerPaymentID.Value));
                    if (CurrentCustomerPayment.IsUnmarked)
                    {
                        AssociatedSales = await Task.Run(() => Sale.GetSalesByUnmarkedPaymentId(CustomerPaymentID.Value).ToBindingList());
                    }
                    else
                    {
                        AssociatedInvoices = await Task.Run(() => SaleInvoice.GetInvoicesByPaymentId(CustomerPaymentID.Value).ToBindingList());
                    }
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboAccount.DisplayMember = "AccountName";
                    cboAccount.ValueMember = "AccountID";
                    cboAccount.DataSource = await Task.Run(() => Account.GetAccounts());
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint CP201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = $"Visualizando cobro: {CurrentCustomerPayment.CustomerPaymentID:D8}";
                txtCustomerPaymentID.Text = CurrentCustomerPayment.CustomerPaymentID.ToString("D8");
                cboBusiness.SelectedValue = CurrentCustomerPayment.BusinessID;
                dtpDate.Value = CurrentCustomerPayment.Date;
                cboCustomer.SelectedValue = CurrentCustomerPayment.CustomerID;
                rbnIsWhite.Checked = !CurrentCustomerPayment.IsUnmarked;
                rbnIsBlack.Checked = CurrentCustomerPayment.IsUnmarked;
                pnlLinkInvoices.Visible = !CurrentCustomerPayment.IsUnmarked;
                pnlLinkSales.Visible = CurrentCustomerPayment.IsUnmarked;
                cboAccount.SelectedValue = CurrentCustomerPayment.AccountID;
                txtAdditionalInformation.Text = CurrentCustomerPayment.AdditionalInformation;
                nudTotalAmount.Value = CurrentCustomerPayment.TotalAmount;
                cboCurrency.SelectedValue = CurrentCustomerPayment.CurrencyID;
                dateTimePickerFechaChequeDiferido.Value = CurrentCustomerPayment.FechaChequeDiferido;



                cboCustomer.Enabled = false;
                pnlCondition.Enabled = false;
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
                    cboAccount.DisplayMember = "AccountName";
                    cboAccount.ValueMember = "AccountID";
                    cboAccount.DataSource = await Task.Run(() => Account.GetAccounts());
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint CP203 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                if (PreselectedSaleID.HasValue)
                {
                    Sale preselectedSale;
                    try
                    {
                        preselectedSale = await Task.Run(() => Sale.GetSaleById(PreselectedSaleID.Value));
                    }
                    catch (Exception dbException)
                    {
                        MessageBox.Show("Error en servidor MySQL."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.AppendLog("Exception at Waypoint CP207 (Flag: MySQL). Message: " + dbException.Message);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    AssociatedSales.Add(preselectedSale);
                    cboBusiness.SelectedValue = preselectedSale.BusinessID;
                    cboCustomer.SelectedValue = preselectedSale.CustomerID;
                    rbnIsWhite.Checked = false;
                    rbnIsBlack.Checked = true;
                    pnlLinkInvoices.Visible = false;
                    pnlLinkSales.Visible = true;
                    cboCustomer.Enabled = false;
                    pnlCondition.Enabled = false;
                    cmsSaleOptions.Enabled = false;
                    btnLinkSale.Enabled = false;
                }
                else
                {
                    cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
                    rbnIsWhite.CheckedChanged += rbnIsWhite_CheckedChanged;
                }
            }
            dgvAssociatedInvoices.DataSource = AssociatedInvoices;
            dgvAssociatedSales.DataSource = AssociatedSales;
        }
        private void CP_CustomerPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentCustomerPayment == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica que haya un cliente seleccionado.
            if (cboCustomer.SelectedItem == null)
            {
                return;
            }

            cboBusiness.SelectedValue = ((Customer)cboCustomer.SelectedItem).BusinessID;
            AssociatedInvoices.Clear();
            AssociatedSales.Clear();
        }
        private void cboCustomer_Validating(object sender, CancelEventArgs e)
        {
            // Borra el texto ingresado si no es un elemento de la lista.
            if (cboCustomer.SelectedItem == null)
            {
                cboCustomer.Text = string.Empty;
                AssociatedInvoices.Clear();
                AssociatedSales.Clear();
            }
        }
        private void rbnIsWhite_CheckedChanged(object sender, EventArgs e)
        {
            pnlLinkInvoices.Visible = rbnIsWhite.Checked;
            pnlLinkSales.Visible = !rbnIsWhite.Checked;
            AssociatedInvoices.Clear();
            AssociatedSales.Clear();
        }

        private void btnLinkInvoice_Click(object sender, EventArgs e)
        {
            // Verifica que haya un cliente seleccionado.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new CP_CustomerPayment_Invoice((int)cboCustomer.SelectedValue, AssociatedInvoices))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var selectedInvoices = form.SelectedInvoices;
                    foreach (var invoice in selectedInvoices)
                    {
                        AssociatedInvoices.Add(invoice);
                    }
                }
            }
        }
        private void btnLinkSale_Click(object sender, EventArgs e)
        {
            // Verifica que haya un cliente seleccionado.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new CP_CustomerPayment_Sale((int)cboCustomer.SelectedValue, AssociatedSales))
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
            // Verifica que haya un cliente seleccionado.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica que el importe sea mayor a cero.
            if (nudTotalAmount.Value == 0)
            {
                MessageBox.Show("El importe debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var payment = new CustomerPayment()
            {
                CustomerPaymentID = (CurrentCustomerPayment == null) ? 0 : CurrentCustomerPayment.CustomerPaymentID,
                BusinessID = (int)cboBusiness.SelectedValue,
                CustomerID = (int)cboCustomer.SelectedValue,
                Date = dtpDate.Value.Date,
                AccountID = (int)cboAccount.SelectedValue,
                TotalAmount = nudTotalAmount.Value,
                CurrencyID = (int)cboCurrency.SelectedValue,
                AdditionalInformation = txtAdditionalInformation.Text.NullIfEmpty(),
                IsUnmarked = rbnIsBlack.Checked,
                FechaChequeDiferido = dateTimePickerFechaChequeDiferido.Value.Date // Añadido aquí
            };

            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        if (CurrentCustomerPayment == null)
                        {
                            // Registro cobro
                            payment.Insert(handler);
                            if (payment.IsUnmarked)
                            {
                                // Asocio el cobro a las ventas seleccionadas
                                foreach (var sale in AssociatedSales)
                                {
                                    Sale.LinkUnmarkedPayment(sale.SaleID, payment.CustomerPaymentID, handler);
                                }
                            }
                            else
                            {
                                // Asocio el cobro a las facturas seleccionadas
                                foreach (var invoice in AssociatedInvoices)
                                {
                                    SaleInvoice.LinkPayment(invoice.SaleInvoiceID, payment.CustomerPaymentID, handler);
                                }
                            }
                        }
                        else
                        {
                            // Actualizo cobro
                            payment.Update(handler);
                            if (payment.IsUnmarked)
                            {
                                // Desasocio el cobro de todas las ventas
                                Sale.UnlinkUnmarkedPaymentFromAll(payment.CustomerPaymentID, handler);
                                // Asocio el cobro a las ventas seleccionadas
                                foreach (var sale in AssociatedSales)
                                {
                                    Sale.LinkUnmarkedPayment(sale.SaleID, payment.CustomerPaymentID, handler);
                                }
                            }
                            else
                            {
                                // Desasocio el cobro de todas las facturas
                                SaleInvoice.UnlinkPaymentFromAll(payment.CustomerPaymentID, handler);
                                // Asocio el cobro a las facturas seleccionadas
                                foreach (var invoice in AssociatedInvoices)
                                {
                                    SaleInvoice.LinkPayment(invoice.SaleInvoiceID, payment.CustomerPaymentID, handler);
                                }
                            }
                        }
                        handler.CommitTransaction();
                    }
                });
                MessageBox.Show("Cobro registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
            }
            catch (Exception dbException)
            {
                MessageBox.Show("Error en servidor MySQL." + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CP205 (Flag: MySQL). Message: " + dbException.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }

        private void cmsItemOpenInvoice_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedInvoice = (SaleInvoice)dgvAssociatedInvoices.SelectedRows[0].DataBoundItem;
            using (var form = new SI_SaleInvoice(selectedInvoice.SaleInvoiceID, SIParameterType.SaleInvoiceID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemUnlinkInvoice_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            AssociatedInvoices.RemoveAt(dgvAssociatedInvoices.SelectedRows[0].Index);
        }
        private void cmsItemOpenSale_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvAssociatedSales.SelectedRows[0].DataBoundItem;
            using (var form = new SA_Sale(selectedSale.SaleID, SAParameterType.SaleID))
            {
                form.ShowDialog();
            }
        }
        private void cmsItemUnlinkSale_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedSales.SelectedRows.Count == 0)
            {
                return;
            }
            AssociatedSales.RemoveAt(dgvAssociatedSales.SelectedRows[0].Index);
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }

    public enum CPParameterType { CustomerPaymentID, PreselectedSaleID }
}
