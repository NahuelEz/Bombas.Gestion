using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PO_PurchaseOrder : Form
    {
        private int? PurchaseOrderID = null;
        private int? PreselectedProviderID = null;
        private PurchaseOrder CurrentPurchaseOrder = null;
        private List<PurchaseOrderItem> Items = new List<PurchaseOrderItem>();
        private bool SafeExit = false;
        
        public PO_PurchaseOrder()
        {
            InitializeComponent();
        }
        public PO_PurchaseOrder(int parameter, POParameterType parameterType)
        {
            switch (parameterType)
            {
                case POParameterType.PurchaseOrderID:
                    {
                        PurchaseOrderID = parameter;
                        break;
                    }
                case POParameterType.PreselectedProviderID:
                    {
                        PreselectedProviderID = parameter;
                        break;
                    }
            }
            InitializeComponent();
        }

        private async void PO_PurchaseOrderUI_Load(object sender, EventArgs e)
        {
            if (PurchaseOrderID.HasValue)
            {
                try
                {
                    CurrentPurchaseOrder = await Task.Run(() => PurchaseOrder.GetPurchaseOrderById(PurchaseOrderID.Value));
                    Items = await Task.Run(() => PurchaseOrderItem.GetItemsByPurchaseOrderId(PurchaseOrderID.Value));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboProvider.DisplayMember = "ProviderName";
                    cboProvider.ValueMember = "ProviderID";
                    cboProvider.DataSource = await Task.Run(() => Provider.GetProvidersLight());
                    cboContact.DisplayMember = "ContactName";
                    cboContact.ValueMember = "ContactID";
                    cboContact.DataSource = await Task.Run(() => ProviderContact.GetContactsByProviderId(CurrentPurchaseOrder.ProviderID));
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint PO201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PO201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando orden de compra: " + CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                txtPurchaseOrderID.Text = CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                cboBusiness.SelectedValue = CurrentPurchaseOrder.BusinessID;
                dtpDate.Value = CurrentPurchaseOrder.Date;
                cboProvider.SelectedValue = CurrentPurchaseOrder.ProviderID;
                if (CurrentPurchaseOrder.ContactID.HasValue)
                {
                    cboContact.SelectedValue = CurrentPurchaseOrder.ContactID.Value;
                }
                else
                {
                    cboContact.SelectedItem = null;
                }
                txtDescription.Text = CurrentPurchaseOrder.Description;
                cboCurrency.SelectedValue = CurrentPurchaseOrder.CurrencyID;
                btnAccept.Enabled = !CurrentPurchaseOrder.PurchaseInvoiceID.HasValue;
                btnSaveAs.Enabled = true;
                ComputeTotals();
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
                    int selectedProviderId = ((Provider)cboProvider.SelectedItem).ProviderID;
                    cboContact.DisplayMember = "ContactName";
                    cboContact.ValueMember = "ContactID";
                    cboContact.DataSource = await Task.Run(() => ProviderContact.GetContactsByProviderId(selectedProviderId));
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint PO202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PO202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                cboProvider.SelectedIndexChanged += cboProvider_SelectedIndexChanged;
                // Verifica si se solicitó preselección de proveedor.
                if (PreselectedProviderID.HasValue)
                {
                    cboProvider.SelectedValue = PreselectedProviderID.Value;
                }
            }
        }
        private void PO_PurchaseOrderUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentPurchaseOrder == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private void btnShowItems_Click(object sender, EventArgs e)
        {
            using (var form = new PO_Items(Items, ((Currency)cboCurrency.SelectedItem).CurrencySymbol))
            {
                form.ShowDialog();
            }
            ComputeTotals();
        }
        private void btnCurrencyConverter_Click(object sender, EventArgs e)
        {
            if (Items.Count == 0)
            {
                MessageBox.Show("La orden de compra no tiene ítems.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    }
                    ComputeTotals();
                }
            }
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (Items.Count == 0)
            {
                MessageBox.Show("La orden de compra debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("La descripción de la orden de compra está incompleta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda orden de compra.
            try
            {
                await SavePurchaseOrderAsync(false);
                this.Text = "Visualizando orden de compra: " + CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                txtPurchaseOrderID.Text = CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint PO203
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO203 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            if (!CurrentPurchaseOrder.ContactID.HasValue)
            {
                MessageBox.Show("Orden de compra guardada."
                    + Environment.NewLine + "No se generó orden de compra PDF porque no hay contacto seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
                return;
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Provider selectedProvider = null;
            ProviderContact selectedContact = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentPurchaseOrder.BusinessID));
                selectedProvider = await Task.Run(() => Provider.GetProviderById(CurrentPurchaseOrder.ProviderID));
                selectedContact = await Task.Run(() => ProviderContact.GetContactById(CurrentPurchaseOrder.ContactID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint PO204
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO204 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta orden de compra PDF.
            try
            {
                await ExportPdfAsync(CurrentPurchaseOrder, Items, selectedBusiness, selectedProvider, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PO205
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO205. Message: " + pdfExportException.Message);
                return;
            }
            MessageBox.Show("Orden de compra guardada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeExit = true;
            this.Close();
        }
        private async void btnSaveAs_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (Items.Count == 0)
            {
                MessageBox.Show("La orden de compra debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("La descripción de la orden de compra está incompleta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda orden de compra.
            try
            {
                await SavePurchaseOrderAsync(true);
                this.Text = "Visualizando orden de compra: " + CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                txtPurchaseOrderID.Text = CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint PO206
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO206 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            if (!CurrentPurchaseOrder.ContactID.HasValue)
            {
                MessageBox.Show("Orden de compra guardada."
                    + Environment.NewLine + "No se generó orden de compra PDF porque no hay contacto seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
                return;
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Provider selectedProvider = null;
            ProviderContact selectedContact = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentPurchaseOrder.BusinessID));
                selectedProvider = await Task.Run(() => Provider.GetProviderById(CurrentPurchaseOrder.ProviderID));
                selectedContact = await Task.Run(() => ProviderContact.GetContactById(CurrentPurchaseOrder.ContactID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint PO207
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO207 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta orden de compra PDF.
            try
            {
                await ExportPdfAsync(CurrentPurchaseOrder, Items, selectedBusiness, selectedProvider, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PO208
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO208. Message: " + pdfExportException.Message);
                return;
            }
            MessageBox.Show("Orden de compra guardada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeExit = true;
            this.Close();
        }
        private async void btnMakePdf_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboContact.SelectedItem == null)
            {
                MessageBox.Show("No se puede exportar la orden de compra porque no hay contacto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Count == 0)
            {
                MessageBox.Show("La orden de compra debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("La descripción de la orden de compra está incompleta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda orden de compra.
            if (CurrentPurchaseOrder == null || !CurrentPurchaseOrder.PurchaseInvoiceID.HasValue)
            {
                try
                {
                    await SavePurchaseOrderAsync(false);
                    this.Text = "Visualizando orden de compra: " + CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                    txtPurchaseOrderID.Text = CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                }
                catch (Exception dbException)
                {
                    // Waypoint PO209
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PO209 (Flag: MySql). Message: " + dbException.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Atención: el documento a exportar corresponde a la última versión guardada de la orden de compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Provider selectedProvider = null;
            ProviderContact selectedContact = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentPurchaseOrder.BusinessID));
                selectedProvider = await Task.Run(() => Provider.GetProviderById(CurrentPurchaseOrder.ProviderID));
                selectedContact = await Task.Run(() => ProviderContact.GetContactById(CurrentPurchaseOrder.ContactID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint PO210
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO210 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta orden de compra PDF.
            string pdfPath = string.Empty;
            try
            {
                pdfPath = await ExportPdfAsync(CurrentPurchaseOrder, Items, selectedBusiness, selectedProvider, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PO211
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO211. Message: " + pdfExportException.Message);
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
                // Waypoint PO212
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO212. Message: " + pdfOpenException.Message);
            }
        }
        private async void btnSendEmail_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboContact.SelectedItem == null)
            {
                MessageBox.Show("No se puede enviar la orden de compra porque no hay contacto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Count == 0)
            {
                MessageBox.Show("La orden de compra debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("La descripción de la orden de compra está incompleta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda orden de compra.
            if (CurrentPurchaseOrder == null || !CurrentPurchaseOrder.PurchaseInvoiceID.HasValue)
            {
                try
                {
                    await SavePurchaseOrderAsync(false);
                    this.Text = "Visualizando orden de compra: " + CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                    txtPurchaseOrderID.Text = CurrentPurchaseOrder.PurchaseOrderID.ToString("D8");
                }
                catch (Exception dbException)
                {
                    // Waypoint PO213
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PO213 (Flag: MySql). Message: " + dbException.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Atención: el documento a exportar corresponde a la última versión guardada de la orden de compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Provider selectedProvider = null;
            ProviderContact selectedContact = null;
            MailServer mailServer = null;
            MailSetting mailSetting = null;
            List<ProviderContact> providerContacts = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentPurchaseOrder.BusinessID));
                selectedProvider = await Task.Run(() => Provider.GetProviderById(CurrentPurchaseOrder.ProviderID));
                selectedContact = await Task.Run(() => ProviderContact.GetContactById(CurrentPurchaseOrder.ContactID.Value));
                mailServer = await Task.Run(() => MailServer.GetMailServerByBusinessId(CurrentPurchaseOrder.BusinessID));
                mailSetting = await Task.Run(() => MailSetting.GetMailSettingByUserIdAndBusinessId(AppEnvironment.CurrentUser.UserID, CurrentPurchaseOrder.BusinessID));
                providerContacts = await Task.Run(() => ProviderContact.GetContactsByProviderId(CurrentPurchaseOrder.ProviderID));
            }
            catch (Exception dbException)
            {
                // Waypoint PO214
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO214 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta orden de compra PDF.
            string pdfPath = string.Empty;
            try
            {
                pdfPath = await ExportPdfAsync(CurrentPurchaseOrder, Items, selectedBusiness, selectedProvider, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PO215
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO215. Message: " + pdfExportException.Message);
                return;
            }
            // Compone mensaje
            string FromAddress = mailServer.EmailAddress;
            string ToAddress = selectedContact.Email;
            string CCAddress = null;
            string[] AuxAttachments = null;
            // Ventana de selección de contactos para reenvío con copia.
            var remainingContacts = providerContacts.Where(c => c.ContactID != selectedContact.ContactID).ToList();
            if (remainingContacts.Count > 0)
            {
                using (var form = new PO_ContactSelector(remainingContacts))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var selectedContacts = form.SelectedContacts;
                        CCAddress = string.Join(" ", selectedContacts.Select(c => c.Email));
                    }
                    else
                    {
                        MessageBox.Show("Envío cancelado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(CCAddress))
            {
                CCAddress = mailSetting.CopyToEmailAddress;
            }
            else
            {
                CCAddress += " " + mailSetting.CopyToEmailAddress;
            }
            // Ventana de edición de mensaje
            string Subject = mailSetting.PurchaseOrderEmailSubject
                .Replace("{proveedor}", selectedProvider.ProviderName)
                .Replace("{tratamiento}", selectedContact.Greeting)
                .Replace("{empresa}", selectedBusiness.BusinessName)
                .Replace("{id_orden}", CurrentPurchaseOrder.PurchaseOrderID.ToString("D8"));
            string Message = mailSetting.PurchaseOrderEmailBody
                .Replace("{proveedor}", selectedProvider.ProviderName)
                .Replace("{tratamiento}", selectedContact.Greeting)
                .Replace("{empresa}", selectedBusiness.BusinessName)
                .Replace("{id_orden}", CurrentPurchaseOrder.PurchaseOrderID.ToString("D8"));
            using (var form = new SHA_MailPreview(FromAddress, ToAddress, CCAddress, Subject, Message))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ToAddress = form.To;
                    CCAddress = form.CC;
                    Subject = form.Subject;
                    Message = form.Message;
                    AuxAttachments = form.Attachments;
                }
                else
                {
                    MessageBox.Show("Envío cancelado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var mailInfo = new MailInformation();
            mailInfo.Message = Message;
            mailInfo.Subject = Subject;
            mailInfo.FromAddress = FromAddress;
            mailInfo.ToAddress = ToAddress.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            mailInfo.CCAddress = CCAddress.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            mailInfo.PdfAttachment = pdfPath;
            mailInfo.AuxAttachments = AuxAttachments;
            // Envia correo electrónico.
            try
            {
                await Mailing.SendMailAsync(mailInfo, selectedBusiness, mailSetting, mailServer);
                MessageBox.Show("Orden de compra enviada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception mailSendingException)
            {
                // Waypoint PO216
                MessageBox.Show("Error al enviar correo electrónico."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + mailSendingException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO216. Message: " + mailSendingException.Message);
            }
        }

        private async void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboBusiness.SelectedValue = ((Provider)cboProvider.SelectedItem).BusinessID;
            int selectedProviderId = ((Provider)cboProvider.SelectedItem).ProviderID;
            try
            {
                cboContact.DataSource = await Task.Run(() => ProviderContact.GetContactsByProviderId(selectedProviderId));
            }
            catch (Exception dbException)
            {
                // Waypoint PO217
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO217 (Flag: MySql). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
            }
        }

        private async Task SavePurchaseOrderAsync(bool ignoreExistingID)
        {
            // Construye objeto "purchase_order".
            var order = new PurchaseOrder()
            {
                PurchaseOrderID = (CurrentPurchaseOrder == null) ? 0 : CurrentPurchaseOrder.PurchaseOrderID,
                BusinessID = (int)cboBusiness.SelectedValue,
                ProviderID = (int)cboProvider.SelectedValue,
                ContactID = (cboContact.SelectedItem == null) ? new int?() : (int)cboContact.SelectedValue,
                Date = ignoreExistingID ? DateTime.Today : dtpDate.Value.Date,
                Description = txtDescription.Text,
                TotalBeforeTax = Items.Sum(i => i.TotalAmount),
                CurrencyID = (int)cboCurrency.SelectedValue,
                // Información complementaria requerida para exportación a PDF.
                CurrencyName = ((Currency)cboCurrency.SelectedItem).CurrencyName
            };
            await Task.Run(() =>
            {
                using (var handler = new DbTransactionHandler())
                {
                    if (CurrentPurchaseOrder == null || ignoreExistingID)
                    {
                        // Registra nueva orden.
                        order.Insert(handler);
                    }
                    else
                    {
                        // Actualiza orden.
                        order.Update(handler);
                        // Elimina ítems existentes.
                        PurchaseOrderItem.DeleteItemsByPurchaseOrderId(order.PurchaseOrderID, handler);
                    }
                    // Registra ítems actualizados.
                    foreach (var item in Items)
                    {
                        item.PurchaseOrderID = order.PurchaseOrderID;
                        item.Insert(handler);
                    }
                    handler.CommitTransaction();
                }
            });
            CurrentPurchaseOrder = order;
        }
        private async Task<string> ExportPdfAsync(PurchaseOrder order, List<PurchaseOrderItem> items, Business business, Provider provider, ProviderContact contact)
        {
            // Comprobación carpeta de destino.
            string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Ordenes de compra " + business.BusinessName) :
                Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, "Ordenes de compra " + business.BusinessName);
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string pdfPath = Path.Combine(destinationFolder, $"Orden de compra {order.PurchaseOrderID:D8} - {provider.ProviderName.RemoveIllegalCharacters()}.pdf");
            // Generación de presupuesto PDF.
            await Task.Run(() => PdfGeneration.ExportPdfPurchaseOrder(order, items, business, provider, contact, pdfPath));
            return pdfPath;
        }
        private void ComputeTotals()
        {
            lblItemsCount.Text = $"{Items.Count} ítem(s)";
            decimal totalBeforeTax = Items.Sum(i => i.TotalAmount);
            decimal vatTotal = Items.Sum(i => (i.TotalAmount * i.VatPercentage / 100M));
            decimal grandTotal = totalBeforeTax + vatTotal;
            txtTotalBeforeTax.Text = totalBeforeTax.ToString("N2");
            txtVatTotal.Text = vatTotal.ToString("N2");
            txtGrandTotal.Text = grandTotal.ToString("N2");
        }
    }

    public enum POParameterType { PurchaseOrderID, PreselectedProviderID }
}
