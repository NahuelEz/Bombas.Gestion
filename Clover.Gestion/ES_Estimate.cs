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
    public partial class ES_Estimate : Form
    {
        private int? EstimateID = null;
        private int? PreselectedCustomerID = null;
        private int? RepairOrderID = null;
        private Estimate CurrentEstimate = null;
        private List<EstimateItem> Items = new List<EstimateItem>();
        private bool SafeExit = false;
        private string EstimateDesc = null;

        public ES_Estimate()
        {
            InitializeComponent();
        }
        public ES_Estimate(int parameter, ESParameterType parameterType)
        {
            switch (parameterType)
            {
                case ESParameterType.EstimateID:
                    {
                        EstimateID = parameter;
                        break;
                    }
                case ESParameterType.PreselectedCustomerID:
                    {
                        PreselectedCustomerID = parameter;
                        break;
                    }
                case ESParameterType.RepairOrderID:
                    {
                        RepairOrderID = parameter;
                        break;
                    }
            }
            InitializeComponent();
        }
        
        private async void ES_Estimate_Load(object sender, EventArgs e)
        {
            // Ajusta formulario en pantalla.
            int availableHeight = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Height > availableHeight)
            {
                this.Height = availableHeight;
                this.CenterToScreen();
            }
            if (EstimateID.HasValue)
            {
                try
                {
                    CurrentEstimate = await Task.Run(() => Estimate.GetEstimateById(EstimateID.Value));
                    Items = await Task.Run(() => EstimateItem.GetItemsByEstimateId(EstimateID.Value));
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboContact.DisplayMember = "ContactName";
                    cboContact.ValueMember = "ContactID";
                    cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(CurrentEstimate.CustomerID));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                    cboPayment.DisplayMember = "PaymentName";
                    cboPayment.ValueMember = "PaymentID";
                    cboPayment.DataSource = await Task.Run(() => Payment.GetPayments());
                    cboDepartment.DisplayMember = "DepartmentName";
                    cboDepartment.ValueMember = "DepartmentID";
                    cboDepartment.DataSource = await Task.Run(() => Department.GetDepartments());
                }
                catch (Exception dbException)
                {
                    // Waypoint ES201
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES201 (Flag: MySql). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando presupuesto: " + CurrentEstimate.EstimateID.ToString("D8");
                txtEstimateID.Text = CurrentEstimate.EstimateID.ToString("D8");
                dtpDate.Value = CurrentEstimate.Date;
                cboDepartment.SelectedValue = CurrentEstimate.DepartmentID;
                cboBusiness.SelectedValue = CurrentEstimate.BusinessID;
                cboCustomer.SelectedValue = CurrentEstimate.CustomerID;
                if (CurrentEstimate.ContactID.HasValue)
                {
                    cboContact.SelectedValue = CurrentEstimate.ContactID.Value;
                }
                else
                {
                    cboContact.SelectedItem = null;
                }
                EstimateDesc = CurrentEstimate.Description;
                nudDiscount.Value = CurrentEstimate.Discount;
                cboCurrency.SelectedValue = CurrentEstimate.CurrencyID;
                cboPayment.SelectedValue = CurrentEstimate.PaymentID;
                if (CurrentEstimate.DeliveryDelay.HasValue)
                {
                    nudDeliveryDelay.Value = CurrentEstimate.DeliveryDelay.Value;
                    nudDeliveryDelay.Enabled = true;
                    label16.Enabled = true;
                    chkDeliveryNotSpecified.Checked = false;
                }
                nudWarrantyMonths.Value = CurrentEstimate.WarrantyMonths;
                nudValidFor.Value = (CurrentEstimate.ExpirationDate.Date - CurrentEstimate.Date.Date).Days;
                lblExpiredWarning.Visible = (DateTime.Today > CurrentEstimate.ExpirationDate.Date);
                switch (CurrentEstimate.Status)
                {
                    case "Activo":
                        {
                            rbnActive.Checked = true;
                            rbnRejected.Checked = false;
                            rbnSold.Checked = false;
                            break;
                        }
                    case "Rechazado":
                        {
                            rbnActive.Checked = false;
                            rbnRejected.Checked = true;
                            rbnSold.Checked = false;
                            break;
                        }
                    case "Vendido":
                        {
                            rbnActive.Checked = false;
                            rbnRejected.Checked = false;
                            rbnSold.Checked = true;
                            rbnActive.Enabled = false;
                            rbnRejected.Enabled = false;
                            rbnSold.Enabled = true;
                            btnAccept.Enabled = false;
                            break;
                        }
                }
                rbnIsWhite.Checked = !CurrentEstimate.IsUnmarked;
                rbnIsBlack.Checked = CurrentEstimate.IsUnmarked;
                chkDontTotalize.Checked = CurrentEstimate.DontTotalize;
                btnSaveAs.Enabled = true;
                btnAccept.Text = "Guardar";
                ComputeTotals();
                dtpDate.ValueChanged += dtpDate_ValueChanged;
                cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
                chkDeliveryNotSpecified.CheckedChanged += chkDeliveryNotSpecified_CheckedChanged;
                nudValidFor.ValueChanged += nudValidFor_ValueChanged;
            }
            else
            {
                try
                {
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    if (((List<Customer>)cboCustomer.DataSource).Count == 0)
                    {
                        MessageBox.Show("No hay clientes registrados en el sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    var selectedCustomer = (Customer)cboCustomer.SelectedItem;
                    cboContact.DisplayMember = "ContactName";
                    cboContact.ValueMember = "ContactID";
                    cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(selectedCustomer.CustomerID));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                    cboPayment.DisplayMember = "PaymentName";
                    cboPayment.ValueMember = "PaymentID";
                    cboPayment.DataSource = await Task.Run(() => Payment.GetPayments());
                    cboPayment.SelectedValue = selectedCustomer.PaymentID;
                    cboDepartment.DisplayMember = "DepartmentName";
                    cboDepartment.ValueMember = "DepartmentID";
                    cboDepartment.DataSource = await Task.Run(() => Department.GetDepartments());
                    cboDepartment.SelectedValue = AppEnvironment.CurrentUser.DepartmentID;
                }
                catch (Exception dbException)
                {
                    // Waypoint ES202
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES202 (Flag: MySql). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                dtpDate.ValueChanged += dtpDate_ValueChanged;
                cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
                chkDeliveryNotSpecified.CheckedChanged += chkDeliveryNotSpecified_CheckedChanged;
                nudValidFor.ValueChanged += nudValidFor_ValueChanged;
                // Verifica si se solicitó preselección de cliente.
                if (PreselectedCustomerID.HasValue)
                {
                    cboCustomer.SelectedValue = PreselectedCustomerID.Value;
                }
                // Verifica si se solicitó presupuestar orden de reparación.
                if (RepairOrderID.HasValue)
                {
                    RepairOrder linkedRepairOrder = null;
                    try
                    {
                        linkedRepairOrder = await Task.Run(() => RepairOrder.GetRepairOrderById(RepairOrderID.Value));
                    }
                    catch (Exception dbException)
                    {
                        // Waypoint ES203
                        MessageBox.Show("Error en servidor MySql."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.AppendLog("Exception at Waypoint ES203 (Flag: MySql). Message: " + dbException.Message);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    cboCustomer.SelectedValue = linkedRepairOrder.CustomerID.Value;
                    var repairItem = new EstimateItem
                    {
                        Description = $"Reparación {linkedRepairOrder.MotorTypeName} {linkedRepairOrder.PumpBrand} {linkedRepairOrder.PumpModel}"
                                           + Environment.NewLine + "Trabajos a realizar: "
                                           + Environment.NewLine + "- Desarme del equipo"
                                           +
                                           (linkedRepairOrder.AppliesForWinding ?
                                           (linkedRepairOrder.RequiresWinding ? (Environment.NewLine + $"- Bobinado motor {linkedRepairOrder.EnginePower} HP.") : (Environment.NewLine + "- Mantenimiento de motor"))
                                           : string.Empty)
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.Bearings)) ? (Environment.NewLine + $"- Rodamientos: {linkedRepairOrder.Bearings}") : string.Empty)
                                           + (linkedRepairOrder.RequiresSandblast ? (Environment.NewLine + "- Arenado") : string.Empty)
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.Locks)) ? (Environment.NewLine + $"- Retenes: {linkedRepairOrder.Locks}") : string.Empty)
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.PaintColor)) ? (Environment.NewLine + $"- Pintura color: {linkedRepairOrder.PaintColor}") : string.Empty)
                                           +
                                           (linkedRepairOrder.SealsApply ?
                                           (linkedRepairOrder.RequiresNewSeals ? (Environment.NewLine + "- Sellos nuevos") : (Environment.NewLine + "- Reparación de sellos"))
                                           : string.Empty)
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.Seals)) ? (Environment.NewLine + $"- Modelo de sellos: {linkedRepairOrder.Seals}") : string.Empty)
                                           + (linkedRepairOrder.RequiresNewCapacitor ? (Environment.NewLine + "- Capacitor nuevo") : string.Empty)
                                           + ((linkedRepairOrder.CapacitorVoltage.HasValue) ? (Environment.NewLine + $"- Voltaje: {linkedRepairOrder.CapacitorVoltage.Value} V") : string.Empty)
                                           + ((linkedRepairOrder.CapacitorCapacity.HasValue) ? (Environment.NewLine + $"- Capacitancia: {linkedRepairOrder.CapacitorCapacity.Value} uF") : string.Empty)
                                           + (linkedRepairOrder.RequiresShaftCladding ? (Environment.NewLine + "- Encamisado de eje") : string.Empty)
                                           + (linkedRepairOrder.RequiresShaftManufacturing ? (Environment.NewLine + "- Fabricación de eje nuevo") : string.Empty)
                                           + ((linkedRepairOrder.CableLenght.HasValue) ? (Environment.NewLine + $"- Cable {linkedRepairOrder.CableLenght.Value} m") : string.Empty)
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.CableSize)) ? (Environment.NewLine + $"- Medida del cable: {linkedRepairOrder.CableSize}") : string.Empty)
                                           + (linkedRepairOrder.RequiresOrings ? (Environment.NewLine + "- Kit de O'rings") : string.Empty)
                                           + (linkedRepairOrder.RequiresJoints ? (Environment.NewLine + "- Juntas nuevas") : string.Empty)
                                           + (linkedRepairOrder.RequiresDielectricOil ? (Environment.NewLine + "- Aceite dieléctrico") : string.Empty)
                                           + (linkedRepairOrder.RequiresNewImpeller ? (Environment.NewLine + "- Impulsor nuevo") : string.Empty)
                                           + (linkedRepairOrder.RequiresPins ? (Environment.NewLine + "- Chavetas nuevas") : string.Empty)
                                           + (linkedRepairOrder.RequiresPackings ? (Environment.NewLine + "- Empaquetaduras nuevas") : string.Empty)
                                           + (linkedRepairOrder.RequiresGrease ? (Environment.NewLine + "- Grasa para caja de rulemanes") : string.Empty)
                                           + Environment.NewLine + "- Limpieza"
                                           + Environment.NewLine + "- Armado"
                                           + Environment.NewLine + "- Pintura"
                                           + Environment.NewLine + "- Pruebas hidráulicas y dinámicas"
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.MissingParts)) ? (Environment.NewLine + $"- Faltantes: {linkedRepairOrder.MissingParts}") : string.Empty)
                                           + ((!string.IsNullOrWhiteSpace(linkedRepairOrder.Notes)) ? (Environment.NewLine + $"Otros: {linkedRepairOrder.Notes}") : string.Empty),
                        Quantity = 1,
                        Amount = 0,
                        TotalAmount = 0,
                        VatID = 5,
                        VatPercentage = 21,
                        DeliveryDelay = null,
                        CustomImage = linkedRepairOrder.DevicePicture
                    };
                    Items.Add(repairItem);
                    ComputeTotals();
                    // Avisa al usuario si no se registró la actualización de desarme.
                    if (linkedRepairOrder.Stage == 0)
                    {
                        const string messageText = "La orden de reparación no tiene registrada la actualización correspondiente al desarme."
                            + "\nAl momento de guardar el presupuesto, el sistema la registrará automáticamente.";
                        MessageBox.Show(messageText, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void ES_Estimate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentEstimate == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private void btnEditDescription_Click(object sender, EventArgs e)
        {
            using (var form = new SHA_TextBox(EstimateDesc, 128))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    EstimateDesc = form.EditedText;
                }
            }
        }
        private void btnOpenDetail_Click(object sender, EventArgs e)
        {
            int customerId = ((Customer)cboCustomer.SelectedItem).CustomerID;
            string currencySymbol = ((Currency)cboCurrency.SelectedItem).CurrencySymbol;

            using (var form = new ES_Items(Items, customerId, currencySymbol))
            {
                form.ShowDialog();
            }

            ComputeTotals();
        }
        private void btnPercentageAdjustment_Click(object sender, EventArgs e)
        {
            if (Items.Count == 0)
            {
                MessageBox.Show("El presupuesto no tiene ítems.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal ratio;
            using (var form = new ES_PercentageAdjustment())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ratio = 1 + form.Percentage / 100M;
                }
                else
                {
                    return;
                }
            }
            foreach (var item in Items)
            {
                item.Amount *= ratio;
                item.TotalAmount = item.Quantity * item.Amount;
            }
            ComputeTotals();
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Count == 0)
            {
                MessageBox.Show("El presupuesto debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda presupuesto.
            try
            {
                await SaveEstimateAsync(false);
                this.Text = "Visualizando presupuesto: " + CurrentEstimate.EstimateID.ToString("D8");
                txtEstimateID.Text = CurrentEstimate.EstimateID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint ES204
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES204 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            if (!CurrentEstimate.ContactID.HasValue)
            {
                MessageBox.Show("Presupuesto guardado."
                    + Environment.NewLine + "No se generó presupuesto PDF porque no hay contacto seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
                return;
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Customer selectedCustomer = null;
            CustomerContact selectedContact = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentEstimate.BusinessID));
                selectedCustomer = await Task.Run(() => Customer.GetCustomerById(CurrentEstimate.CustomerID));
                selectedContact = await Task.Run(() => CustomerContact.GetContactById(CurrentEstimate.ContactID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint ES205
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES205 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta presupuesto pdf.
            try
            {
                await ExportPdfAsync(CurrentEstimate, Items, selectedBusiness, selectedCustomer, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint ES206
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES206. Message: " + pdfExportException.Message);
                return;
            }
            MessageBox.Show("Presupuesto guardado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeExit = true;
            this.Close();
        }
        private async void btnSaveAs_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Count == 0)
            {
                MessageBox.Show("El presupuesto debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda presupuesto.
            try
            {
                await SaveEstimateAsync(true);
                this.Text = "Visualizando presupuesto: " + CurrentEstimate.EstimateID.ToString("D8");
                txtEstimateID.Text = CurrentEstimate.EstimateID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint ES207
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES207 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            if (!CurrentEstimate.ContactID.HasValue)
            {
                MessageBox.Show("Presupuesto guardado."
                    + Environment.NewLine + "No se generó presupuesto PDF porque no hay contacto seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
                return;
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Customer selectedCustomer = null;
            CustomerContact selectedContact = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentEstimate.BusinessID));
                selectedCustomer = await Task.Run(() => Customer.GetCustomerById(CurrentEstimate.CustomerID));
                selectedContact = await Task.Run(() => CustomerContact.GetContactById(CurrentEstimate.ContactID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint ES208
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES208 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta presupuesto pdf.
            try
            {
                await ExportPdfAsync(CurrentEstimate, Items, selectedBusiness, selectedCustomer, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint ES209
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES209. Message: " + pdfExportException.Message);
                return;
            }
            MessageBox.Show("Presupuesto guardado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeExit = true;
            this.Close();
        }
        private async void btnMakePDF_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboContact.SelectedItem == null)
            {
                MessageBox.Show("No se puede exportar el presupuesto porque no hay contacto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Count == 0)
            {
                MessageBox.Show("El presupuesto debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda presupuesto.
            if (CurrentEstimate == null || CurrentEstimate.Status != "Vendido")
            {
                try
                {
                    await SaveEstimateAsync(false);
                    this.Text = "Visualizando presupuesto: " + CurrentEstimate.EstimateID.ToString("D8");
                    txtEstimateID.Text = CurrentEstimate.EstimateID.ToString("D8");
                }
                catch (Exception dbException)
                {
                    // Waypoint ES210
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES210 (Flag: MySql). Message: " + dbException.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Atención: el documento a exportar corresponde a la última versión guardada del presupuesto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Customer selectedCustomer = null;
            CustomerContact selectedContact = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentEstimate.BusinessID));
                selectedCustomer = await Task.Run(() => Customer.GetCustomerById(CurrentEstimate.CustomerID));
                selectedContact = await Task.Run(() => CustomerContact.GetContactById(CurrentEstimate.ContactID.Value));
            }
            catch (Exception dbException)
            {
                // Waypoint ES211
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES211 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta presupuesto pdf.
            string pdfPath = string.Empty;
            try
            {
                pdfPath = await ExportPdfAsync(CurrentEstimate, Items, selectedBusiness, selectedCustomer, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint ES212
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES212. Message: " + pdfExportException.Message);
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
                // Waypoint ES213
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES213. Message: " + pdfOpenException.Message);
            }
        }
        private async void btnSendEmail_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboCustomer.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboContact.SelectedItem == null)
            {
                MessageBox.Show("No se puede enviar el presupuesto porque no hay contacto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Items.Count == 0)
            {
                MessageBox.Show("El presupuesto debe contener al menos un ítem.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda presupuesto.
            if (CurrentEstimate == null || CurrentEstimate.Status != "Vendido")
            {
                try
                {
                    await SaveEstimateAsync(false);
                    this.Text = "Visualizando presupuesto: " + CurrentEstimate.EstimateID.ToString("D8");
                    txtEstimateID.Text = CurrentEstimate.EstimateID.ToString("D8");
                }
                catch (Exception dbException)
                {
                    // Waypoint ES214
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES214 (Flag: MySql). Message: " + dbException.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Atención: el documento a exportar corresponde a la última versión guardada del presupuesto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Recupera información adicional.
            Business selectedBusiness = null;
            Customer selectedCustomer = null;
            CustomerContact selectedContact = null;
            MailServer mailServer = null;
            MailSetting mailSetting = null;
            List<CustomerContact> customerContacts = null;
            try
            {
                selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentEstimate.BusinessID));
                selectedCustomer = await Task.Run(() => Customer.GetCustomerById(CurrentEstimate.CustomerID));
                selectedContact = await Task.Run(() => CustomerContact.GetContactById(CurrentEstimate.ContactID.Value));
                mailServer = await Task.Run(() => MailServer.GetMailServerByBusinessId(CurrentEstimate.BusinessID));
                mailSetting = await Task.Run(() => MailSetting.GetMailSettingByUserIdAndBusinessId(AppEnvironment.CurrentUser.UserID, CurrentEstimate.BusinessID));
                customerContacts = await Task.Run(() => CustomerContact.GetContactsByCustomerId(CurrentEstimate.CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint ES215
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES215 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta presupuesto pdf.
            string pdfPath = string.Empty;
            try
            {
                pdfPath = await ExportPdfAsync(CurrentEstimate, Items, selectedBusiness, selectedCustomer, selectedContact);
            }
            catch (Exception pdfExportException)
            {
                // Waypoint ES216
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES216. Message: " + pdfExportException.Message);
                return;
            }
            // Compone mensaje
            string FromAddress = mailServer.EmailAddress;
            string ToAddress = selectedContact.Email;
            string CCAddress = null;
            string[] AuxAttachments = null;
            // Ventana de selección de contactos para reenvío con copia.
            var remainingContacts = customerContacts.Where(c => c.ContactID != selectedContact.ContactID).ToList();
            if (remainingContacts.Count > 0)
            {
                using (var form = new ES_ContactSelector(remainingContacts))
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
            string Subject = mailSetting.EstimateEmailSubject
                .Replace("{cliente}", selectedCustomer.CustomerName)
                .Replace("{tratamiento}", selectedContact.Greeting)
                .Replace("{empresa}", selectedBusiness.BusinessName)
                .Replace("{validez}", CurrentEstimate.ExpirationDate.ToString("dd/MM/yyyy"))
                .Replace("{id_presupuesto}", CurrentEstimate.EstimateID.ToString("D8"));
            string Message = mailSetting.EstimateEmailBody
                .Replace("{cliente}", selectedCustomer.CustomerName)
                .Replace("{tratamiento}", selectedContact.Greeting)
                .Replace("{empresa}", selectedBusiness.BusinessName)
                .Replace("{validez}", CurrentEstimate.ExpirationDate.ToString("dd/MM/yyyy"))
                .Replace("{id_presupuesto}", CurrentEstimate.EstimateID.ToString("D8"));
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
            var mailInfo = new MailInformation
            {
                Message = Message,
                Subject = Subject,
                FromAddress = FromAddress,
                ToAddress = ToAddress.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
                CCAddress = CCAddress.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
                PdfAttachment = pdfPath,
                AuxAttachments = AuxAttachments
            };
            // Envia correo electrónico.
            try
            {
                await Mailing.SendMailAsync(mailInfo, selectedBusiness, mailSetting, mailServer);
                MessageBox.Show("Presupuesto enviado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception mailSendingException)
            {
                // Waypoint ES217
                MessageBox.Show("Error al enviar correo electrónico."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + mailSendingException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES217. Message: " + mailSendingException.Message);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            lblExpiredWarning.Visible = (DateTime.Today > dtpDate.Value.Date.AddDays((int)(nudValidFor.Value)));
        }
        private async void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica que haya un cliente seleccionado.
            if (cboCustomer.SelectedItem == null)
            {
                return;
            }

            var selectedCustomer = (Customer)cboCustomer.SelectedItem;
            cboBusiness.SelectedValue = selectedCustomer.BusinessID;
            cboPayment.SelectedValue = selectedCustomer.PaymentID;
            try
            {
                cboContact.DisplayMember = "ContactName";
                cboContact.ValueMember = "ContactID";
                cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(selectedCustomer.CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint ES218
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES218 (Flag: MySql). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
            }
        }
        private void cboCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Borra el texto ingresado si no es un elemento de la lista.
            if (cboCustomer.SelectedItem == null)
            {
                cboCustomer.Text = string.Empty;
                cboContact.DataSource = null;
            }
        }
        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            ComputeTotals();
        }
        private void chkDeliveryNotSpecified_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeliveryNotSpecified.Checked)
            {
                nudDeliveryDelay.Enabled = false;
                label16.Enabled = false;
            }
            else
            {
                nudDeliveryDelay.Enabled = true;
                label16.Enabled = true;
            }
        }
        private void nudValidFor_ValueChanged(object sender, EventArgs e)
        {
            lblExpiredWarning.Visible = (DateTime.Today > dtpDate.Value.Date.AddDays((int)(nudValidFor.Value)));
        }

        private async Task SaveEstimateAsync(bool ignoreExistingID)
        {
            // Construye objeto "estimate".
            var estimate = new Estimate()
            {
                EstimateID = (CurrentEstimate == null) ? 0 : CurrentEstimate.EstimateID,
                BusinessID = (int)cboBusiness.SelectedValue,
                CustomerID = (int)cboCustomer.SelectedValue,
                ContactID = (cboContact.SelectedItem == null) ? new int?() : (int)cboContact.SelectedValue,
                Date = (ignoreExistingID) ? DateTime.Today : dtpDate.Value.Date,
                Discount = nudDiscount.Value,
                Description = EstimateDesc.NullIfEmpty(),
                TotalBeforeTax = (Items.Sum(i => i.TotalAmount) * (100M - nudDiscount.Value) / 100M),
                CurrencyID = (int)cboCurrency.SelectedValue,
                PaymentID = (int)cboPayment.SelectedValue,
                DeliveryDelay = (chkDeliveryNotSpecified.Checked) ? new int?() : (int)nudDeliveryDelay.Value,
                WarrantyMonths = (int)nudWarrantyMonths.Value,
                ExpirationDate = (ignoreExistingID) ? DateTime.Today.AddDays((int)nudValidFor.Value) : dtpDate.Value.Date.AddDays((int)nudValidFor.Value),
                Status = (rbnActive.Checked || ignoreExistingID) ? "Activo" : "Rechazado",
                DepartmentID = (int)cboDepartment.SelectedValue,
                IsUnmarked = rbnIsBlack.Checked,
                DontTotalize = chkDontTotalize.Checked,
                // Información complementaria requerida para exportación a PDF.
                CurrencyName = ((Currency)cboCurrency.SelectedItem).CurrencyName,
                PaymentName = ((Payment)cboPayment.SelectedItem).PaymentName,
            };
            await Task.Run(() =>
            {
                using (var handler = new DbTransactionHandler())
                {
                    if (CurrentEstimate == null || ignoreExistingID)
                    {
                        // Registra nuevo presupuesto.
                        estimate.Insert(handler);
                        // Actualiza orden de reparación (si corresponde).
                        if (CurrentEstimate == null && RepairOrderID.HasValue)
                        {
                            var linkedRepairOrder = RepairOrder.GetRepairOrderById(RepairOrderID.Value, handler);
                            // Registra automáticamente el desarme si corresponde.
                            if (linkedRepairOrder.Stage == 0)
                            {
                                var update1 = new ProgressUpdate
                                {
                                    RepairOrderID = linkedRepairOrder.RepairOrderID,
                                    UserID = AppEnvironment.CurrentUser.UserID,
                                    Date = DateTime.Now,
                                    UpdateTypeID = 2
                                };
                                update1.Insert(handler);
                                linkedRepairOrder.Stage = 1;
                            }
                            // Registra actualización de progreso.
                            var update2 = new ProgressUpdate
                            {
                                RepairOrderID = linkedRepairOrder.RepairOrderID,
                                UserID = AppEnvironment.CurrentUser.UserID,
                                Date = DateTime.Now,
                                UpdateTypeID = 12
                            };
                            update2.Insert(handler);
                            // Actualiza orden de reparación.
                            linkedRepairOrder.EstimateID = estimate.EstimateID;
                            if (linkedRepairOrder.Stage == 1)
                            {
                                // Si está en la etapa correcta (puede cotizarse también en etapa 2), además actualiza estado.
                                linkedRepairOrder.Status = "Esperando aprobación";
                            }
                            linkedRepairOrder.Update(handler);
                        }
                    }
                    else
                    {
                        // Actualiza presupuesto.
                        estimate.Update(handler);
                        // Elimina ítems existentes.
                        EstimateItem.DeleteItemsByEstimateId(estimate.EstimateID, handler);
                    }
                    // Registra ítems actualizados.
                    int itemNumber = 1;
                    foreach (var item in Items)
                    {
                        item.EstimateID = estimate.EstimateID;
                        item.ItemNumber = itemNumber;
                        item.Insert(handler);
                        itemNumber++;
                    }
                    handler.CommitTransaction();
                }
            });
            CurrentEstimate = estimate;
        }
        private async Task<string> ExportPdfAsync(Estimate estimate, List<EstimateItem> items, Business business, Customer customer, CustomerContact contact)
        {
            // Comprobación de opciones de exportación.
            bool keepItemTogether;
            switch (AppEnvironment.CurrentSettings.PdfKeepItemTogether)
            {
                case "Yes":
                    keepItemTogether = true;
                    break;
                case "No":
                    keepItemTogether = false;
                    break;
                case "Ask":
                default:
                    var prompt = MessageBox.Show("¿Desea priorizar items enteros? Caso contrario, se priorizará páginas completas.", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    keepItemTogether = (prompt == DialogResult.Yes);
                    break;
            }
            // Comprobación carpeta de destino.
            string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Presupuestos " + business.BusinessName) :
                Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, "Presupuestos " + business.BusinessName);
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string pdfPath = Path.Combine(destinationFolder, $"Presupuesto {estimate.EstimateID:D8} - {customer.CustomerName.RemoveIllegalCharacters()}.pdf");
            // Generación de presupuesto PDF.
            await Task.Run(() => PdfGeneration.ExportPdfEstimate(estimate, items, business, customer, contact, pdfPath, keepItemTogether));
            return pdfPath;
        }
        private void ComputeTotals()
        {
            lblItemsCount.Text = $"{Items.Count} ítem(s)";
            decimal totalAmount = Items.Sum(i => i.TotalAmount);
            decimal totalBeforeTax = totalAmount * (100M - nudDiscount.Value) / 100M;
            decimal vatTotal = Items.Sum(i => (i.TotalAmount * i.VatPercentage / 100M)) * (100M - nudDiscount.Value) / 100M;
            decimal grandTotal = totalBeforeTax + vatTotal;
            txtTotalAmount.Text = totalAmount.ToString("N2");
            txtTotalBeforeTax.Text = totalBeforeTax.ToString("N2");
            txtVatTotal.Text = vatTotal.ToString("N2");
            txtGrandTotal.Text = grandTotal.ToString("N2");
        }
    }

    public enum ESParameterType { EstimateID, PreselectedCustomerID, RepairOrderID }
}
