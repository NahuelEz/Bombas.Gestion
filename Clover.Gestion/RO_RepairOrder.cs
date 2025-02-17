using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class RO_RepairOrder : Form
    {
        private int? RepairOrderID = null;
        private RepairOrder CurrentRepairOrder = null;
        private bool SafeExit = false;
        private Image DevicePicture = null;

        public RO_RepairOrder(int? RepairOrderID = null)
        {
            this.RepairOrderID = RepairOrderID;
            InitializeComponent();
        }

        private async void RO_RepairOrder_Load(object sender, EventArgs e)
        {
            // Fit screen
            int availableHeight = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Height > availableHeight)
            {
                this.Height = availableHeight;
                this.CenterToScreen();
            }
            if (RepairOrderID.HasValue)
            {
                try
                {
                    CurrentRepairOrder = await Task.Run(() => RepairOrder.GetRepairOrderById(RepairOrderID.Value));
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboMotorType.DisplayMember = "MotorTypeName";
                    cboMotorType.ValueMember = "MotorTypeID";
                    cboMotorType.DataSource = await Task.Run(() => MotorType.GetMotorTypes());
                    cboSealsDeliveredTo.DisplayMember = "UserName";
                    cboSealsDeliveredTo.ValueMember = "UserID";
                    cboSealsDeliveredTo.DataSource = await Task.Run(() => User.GetUsersByDepartmentId(8));
                }
                catch (Exception dbException)
                {
                    // Waypoint RO201
                    MessageBox.Show("(RO201) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RO201 (Flag: MySql). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = $"Visualizando orden: {CurrentRepairOrder.RepairOrderID:D8}";
                txtRepairOrderID.Text = CurrentRepairOrder.RepairOrderID.ToString("D8");
                txtRefNumber.Text = CurrentRepairOrder.RefNumber;
                dtpDate.Value = CurrentRepairOrder.Date;
                if (CurrentRepairOrder.CustomerID.HasValue)
                {
                    cboCustomer.SelectedValue = CurrentRepairOrder.CustomerID.Value;
                }
                else
                {
                    cboCustomer.SelectedItem = null;
                    cboCustomer.Text = CurrentRepairOrder.CustomerName;
                }
                txtPhoneNumber.Text = CurrentRepairOrder.PhoneNumber;
                txtDeliveryNoteNumberCustomer.Text = CurrentRepairOrder.DeliveryNoteNumberCustomer;
                txtDeliveryNoteNumber.Text = CurrentRepairOrder.DeliveryNoteNumber;
                txtInvoiceNumber.Text = CurrentRepairOrder.InvoiceNumber;
                if (CurrentRepairOrder.MotorTypeID.HasValue)
                {
                    cboMotorType.SelectedValue = CurrentRepairOrder.MotorTypeID.Value;
                }
                else
                {
                    cboMotorType.SelectedItem = null;
                    cboMotorType.Text = CurrentRepairOrder.MotorTypeName;
                }
                txtPumpBrand.Text = CurrentRepairOrder.PumpBrand;
                txtPumpModel.Text = CurrentRepairOrder.PumpModel;
                rbnIsSinglePhase.Checked = CurrentRepairOrder.IsSinglePhase;
                rbnIsThreePhase.Checked = !CurrentRepairOrder.IsSinglePhase;
                rbnWindingYes.Checked = (CurrentRepairOrder.AppliesForWinding && CurrentRepairOrder.RequiresWinding);
                rbnWindingNo.Checked = (CurrentRepairOrder.AppliesForWinding && !CurrentRepairOrder.RequiresWinding);
                rbnWindingNotApplies.Checked = !CurrentRepairOrder.AppliesForWinding;
                txtEnginePower.Text = CurrentRepairOrder.EnginePower;
                txtBearings.Text = CurrentRepairOrder.Bearings;
                rbnSandblastYes.Checked = CurrentRepairOrder.RequiresSandblast;
                rbnSandblastNo.Checked = !CurrentRepairOrder.RequiresSandblast;
                txtLocks.Text = CurrentRepairOrder.Locks;
                txtPaintColor.Text = CurrentRepairOrder.PaintColor;
                rbnNewSeals.Checked = (CurrentRepairOrder.SealsApply && CurrentRepairOrder.RequiresNewSeals);
                rbnRepairSeals.Checked = (CurrentRepairOrder.SealsApply && !CurrentRepairOrder.RequiresNewSeals);
                rbnSealsNotApply.Checked = !CurrentRepairOrder.SealsApply;
                txtSeals.Text = CurrentRepairOrder.Seals;
                if (CurrentRepairOrder.SealsDeliveredToID.HasValue)
                {
                    cboSealsDeliveredTo.SelectedValue = CurrentRepairOrder.SealsDeliveredToID.Value;
                }
                else
                {
                    cboSealsDeliveredTo.SelectedItem = null;
                    cboSealsDeliveredTo.Text = CurrentRepairOrder.SealsDeliveredToName;
                }
                rbnCapacitorYes.Checked = CurrentRepairOrder.RequiresNewCapacitor;
                rbnCapacitorNo.Checked = !CurrentRepairOrder.RequiresNewCapacitor;
                txtCapacitorVoltage.Text = CurrentRepairOrder.CapacitorVoltage.ToString();
                txtCapacitorCapacity.Text = CurrentRepairOrder.CapacitorCapacity.ToString();
                rbnShaftCladdingYes.Checked = CurrentRepairOrder.RequiresShaftCladding;
                rbnShaftCladdingNo.Checked = !CurrentRepairOrder.RequiresShaftCladding;
                rbnShaftManufacturingYes.Checked = CurrentRepairOrder.RequiresShaftManufacturing;
                rbnShaftManufacturingNo.Checked = !CurrentRepairOrder.RequiresShaftManufacturing;
                txtCableLenght.Text = CurrentRepairOrder.CableLenght.ToString();
                txtCableSize.Text = CurrentRepairOrder.CableSize;
                rbnOringsYes.Checked = CurrentRepairOrder.RequiresOrings;
                rbnOringsNo.Checked = !CurrentRepairOrder.RequiresOrings;
                rbnJointsYes.Checked = CurrentRepairOrder.RequiresJoints;
                rbnJointsNo.Checked = !CurrentRepairOrder.RequiresJoints;
                rbnDielectricOilYes.Checked = CurrentRepairOrder.RequiresDielectricOil;
                rbnDielectricOilNo.Checked = !CurrentRepairOrder.RequiresDielectricOil;
                rbnImpellerYes.Checked = CurrentRepairOrder.RequiresNewImpeller;
                rbnImpellerNo.Checked = !CurrentRepairOrder.RequiresNewImpeller;
                rbnPinsYes.Checked = CurrentRepairOrder.RequiresPins;
                rbnPinsNo.Checked = !CurrentRepairOrder.RequiresPins;
                rbnPackingsYes.Checked = CurrentRepairOrder.RequiresPackings;
                rbnPackingsNo.Checked = !CurrentRepairOrder.RequiresPackings;
                rbnGreaseYes.Checked = CurrentRepairOrder.RequiresGrease;
                rbnGreaseNo.Checked = !CurrentRepairOrder.RequiresGrease;
                txtMissingParts.Text = CurrentRepairOrder.MissingParts;
                txtNotes.Text = CurrentRepairOrder.Notes;
                txtStorageLocation.Text = CurrentRepairOrder.StorageLocation;
                DevicePicture = CurrentRepairOrder.DevicePicture.ToImage();
                btnMakeCopy.Enabled = true;
                btnMakePDF.Enabled = true;
            }
            else
            {
                try
                {
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboMotorType.DisplayMember = "MotorTypeName";
                    cboMotorType.ValueMember = "MotorTypeID";
                    cboMotorType.DataSource = await Task.Run(() => MotorType.GetMotorTypes());
                    cboSealsDeliveredTo.DisplayMember = "UserName";
                    cboSealsDeliveredTo.ValueMember = "UserID";
                    cboSealsDeliveredTo.DataSource = await Task.Run(() => User.GetUsersByDepartmentId(8));
                }
                catch (Exception dbException)
                {
                    // Waypoint RO202
                    MessageBox.Show("(RO202) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RO202 (Flag: MySql). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                cboCustomer.Text = string.Empty;
                cboMotorType.Text = string.Empty;
                cboSealsDeliveredTo.Text = string.Empty;
            }
        }
        private void RO_RepairOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentRepairOrder == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            if (CurrentRepairOrder != null && (CurrentRepairOrder.EstimateID.HasValue || CurrentRepairOrder.Approved || CurrentRepairOrder.Completed))
            {
                var prompt = MessageBox.Show("La orden que está por modificar ya fue cotizada, aprobada o completada. ¿Desea continuar?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (prompt != DialogResult.Yes)
                {
                    return;
                }
            }
            // Validación de campos obligatorios.
            if (string.IsNullOrWhiteSpace(cboCustomer.Text) || (cboCustomer.SelectedItem == null && string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                || string.IsNullOrWhiteSpace(cboMotorType.Text) || string.IsNullOrWhiteSpace(txtPumpBrand.Text)
                || string.IsNullOrWhiteSpace(txtPumpModel.Text) || (rbnCapacitorYes.Checked && (string.IsNullOrWhiteSpace(txtCapacitorVoltage.Text) || string.IsNullOrWhiteSpace(txtCapacitorCapacity.Text)))
                || string.IsNullOrWhiteSpace(txtStorageLocation.Text))
            {
                MessageBox.Show("Uno o mas campos obligatorios están incompletos."
                    + Environment.NewLine + Environment.NewLine + "El campo Teléfono también es obligatorio si el cliente especificado no está registrado."
                    + Environment.NewLine + "Los campos Voltaje y Capacitancia también son obligatorios si se debe reemplazar capacitor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validación de formato de campos numéricos.
            if ((!string.IsNullOrWhiteSpace(txtCapacitorVoltage.Text) && !int.TryParse(txtCapacitorVoltage.Text, out int result1))
                || (!string.IsNullOrWhiteSpace(txtCapacitorCapacity.Text) && !int.TryParse(txtCapacitorCapacity.Text, out int result2))
                || (!string.IsNullOrWhiteSpace(txtCableLenght.Text) && !int.TryParse(txtCableLenght.Text, out int result3)))
            {
                MessageBox.Show("Uno o mas campos numéricos no tienen el formato correcto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Construye objeto "repair_order".
            var order = new RepairOrder()
            {
                RepairOrderID = (CurrentRepairOrder == null) ? 0 : CurrentRepairOrder.RepairOrderID,
                RefNumber = txtRefNumber.Text.NullIfEmpty(),
                Date = dtpDate.Value.Date,
                CustomerID = (cboCustomer.SelectedItem == null) ? new int?() : ((Customer)cboCustomer.SelectedItem).CustomerID,
                CustomerName = (cboCustomer.SelectedItem == null) ? cboCustomer.Text : null,
                PhoneNumber = txtPhoneNumber.Text.NullIfEmpty(),
                DeliveryNoteNumberCustomer = txtDeliveryNoteNumberCustomer.Text.NullIfEmpty(),
                MotorTypeID = (cboMotorType.SelectedItem == null) ? new int?() : ((MotorType)cboMotorType.SelectedItem).MotorTypeID,
                MotorTypeName = (cboMotorType.SelectedItem == null) ? cboMotorType.Text : null,
                PumpBrand = txtPumpBrand.Text,
                PumpModel = txtPumpModel.Text,
                IsSinglePhase = rbnIsSinglePhase.Checked,
                RequiresWinding = rbnWindingYes.Checked,
                EnginePower = txtEnginePower.Text.NullIfEmpty(),
                Bearings = txtBearings.Text.NullIfEmpty(),
                RequiresSandblast = rbnSandblastYes.Checked,
                Locks = txtLocks.Text.NullIfEmpty(),
                PaintColor = txtPaintColor.Text.NullIfEmpty(),
                RequiresNewSeals = rbnNewSeals.Checked,
                Seals = txtSeals.Text.NullIfEmpty(),
                SealsDeliveredToID = (cboSealsDeliveredTo.SelectedItem == null) ? new int?() : ((User)cboSealsDeliveredTo.SelectedItem).UserID,
                SealsDeliveredToName = (cboSealsDeliveredTo.SelectedItem == null) ? cboSealsDeliveredTo.Text : null,
                RequiresNewCapacitor = rbnCapacitorYes.Checked,
                CapacitorVoltage = txtCapacitorVoltage.Text.ParseInteger(),
                CapacitorCapacity = txtCapacitorCapacity.Text.ParseInteger(),
                RequiresShaftCladding = rbnShaftCladdingYes.Checked,
                RequiresShaftManufacturing = rbnShaftManufacturingYes.Checked,
                CableLenght = txtCableLenght.Text.ParseInteger(),
                CableSize = txtCableSize.Text.NullIfEmpty(),
                RequiresOrings = rbnOringsYes.Checked,
                RequiresJoints = rbnJointsYes.Checked,
                RequiresDielectricOil = rbnDielectricOilYes.Checked,
                RequiresNewImpeller = rbnImpellerYes.Checked,
                RequiresPins = rbnPinsYes.Checked,
                RequiresPackings = rbnPackingsYes.Checked,
                RequiresGrease = rbnGreaseYes.Checked,
                MissingParts = txtMissingParts.Text.NullIfEmpty(),
                Notes = txtNotes.Text.NullIfEmpty(),
                StorageLocation = txtStorageLocation.Text,
                Status = (CurrentRepairOrder == null) ? "Esperando desarme" : CurrentRepairOrder.Status,
                PriorityID = (CurrentRepairOrder == null) ? 3 : CurrentRepairOrder.PriorityID,
                Approved = (CurrentRepairOrder == null) ? false : CurrentRepairOrder.Approved,
                Completed = (CurrentRepairOrder == null) ? false : CurrentRepairOrder.Completed,
                Stage = (CurrentRepairOrder == null) ? 0 : CurrentRepairOrder.Stage,
                AppliesForWinding = !rbnWindingNotApplies.Checked,
                SealsApply = !rbnSealsNotApply.Checked,
                DeliveryNoteNumber = txtDeliveryNoteNumber.Text.NullIfEmpty(),
                InvoiceNumber = txtInvoiceNumber.Text.NullIfEmpty(),
                ApprovalDate = (CurrentRepairOrder == null) ? null : CurrentRepairOrder.ApprovalDate,
                EstimateID = (CurrentRepairOrder == null) ? null : CurrentRepairOrder.EstimateID,
                DevicePicture = DevicePicture.ToByteArray()
            };
            if (CurrentRepairOrder == null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var handler = new DbTransactionHandler())
                        {
                            order.Insert(handler);
                            var progressUpdate = new ProgressUpdate()
                            {
                                RepairOrderID = order.RepairOrderID,
                                UserID = AppEnvironment.CurrentUser.UserID,
                                Date = DateTime.Now,
                                UpdateTypeID = 1,
                                Notes = "Registrado automáticamente."
                            };
                            progressUpdate.Insert(handler);
                            handler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Orden de reparación registrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint RO203
                    MessageBox.Show("(RO203) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RO203 (Flag: MySql). Message: " + dbException.Message);
                    RepairOrderID = null;
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => order.Update());
                    MessageBox.Show("Orden de reparación actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint RO204
                    MessageBox.Show("(RO204) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RO204 (Flag: MySql). Message: " + dbException.Message);
                }
            }
        }
        private void btnProgressManager_Click(object sender, EventArgs e)
        {
            if (!RepairOrderID.HasValue)
            {
                MessageBox.Show("La orden de reparación debe estar registrada para generar actualizaciones.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var form = new RO_ProgressManager(RepairOrderID.Value))
            {
                form.ShowDialog();
            }
        }
        private async void btnMakeCopy_Click(object sender, EventArgs e)
        {
            // Validación de campos obligatorios.
            if (string.IsNullOrWhiteSpace(cboCustomer.Text) || (cboCustomer.SelectedItem == null && string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                || string.IsNullOrWhiteSpace(cboMotorType.Text) || string.IsNullOrWhiteSpace(txtPumpBrand.Text)
                || string.IsNullOrWhiteSpace(txtPumpModel.Text) || (rbnCapacitorYes.Checked && (string.IsNullOrWhiteSpace(txtCapacitorVoltage.Text) || string.IsNullOrWhiteSpace(txtCapacitorCapacity.Text)))
                || string.IsNullOrWhiteSpace(txtStorageLocation.Text))
            {
                MessageBox.Show("Uno o mas campos obligatorios están incompletos."
                    + Environment.NewLine + Environment.NewLine + "El campo Teléfono también es obligatorio si el cliente especificado no está registrado."
                    + Environment.NewLine + "Los campos Voltaje y Capacitancia también son obligatorios si se debe reemplazar capacitor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validación de formato de campos numéricos.
            if ((!string.IsNullOrWhiteSpace(txtCapacitorVoltage.Text) && !int.TryParse(txtCapacitorVoltage.Text, out int result1))
                || (!string.IsNullOrWhiteSpace(txtCapacitorCapacity.Text) && !int.TryParse(txtCapacitorCapacity.Text, out int result2))
                || (!string.IsNullOrWhiteSpace(txtCableLenght.Text) && !int.TryParse(txtCableLenght.Text, out int result3)))
            {
                MessageBox.Show("Uno o mas campos numéricos no tienen el formato correcto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Construye objeto "repair_order".
            var order = new RepairOrder()
            {
                RefNumber = txtRefNumber.Text.NullIfEmpty(),
                Date = DateTime.Today,
                CustomerID = (cboCustomer.SelectedItem == null) ? new int?() : ((Customer)cboCustomer.SelectedItem).CustomerID,
                CustomerName = (cboCustomer.SelectedItem == null) ? cboCustomer.Text : null,
                PhoneNumber = txtPhoneNumber.Text.NullIfEmpty(),
                DeliveryNoteNumberCustomer = txtDeliveryNoteNumberCustomer.Text.NullIfEmpty(),
                MotorTypeID = (cboMotorType.SelectedItem == null) ? new int?() : ((MotorType)cboMotorType.SelectedItem).MotorTypeID,
                MotorTypeName = (cboMotorType.SelectedItem == null) ? cboMotorType.Text : null,
                PumpBrand = txtPumpBrand.Text,
                PumpModel = txtPumpModel.Text,
                IsSinglePhase = rbnIsSinglePhase.Checked,
                RequiresWinding = rbnWindingYes.Checked,
                EnginePower = txtEnginePower.Text.NullIfEmpty(),
                Bearings = txtBearings.Text.NullIfEmpty(),
                RequiresSandblast = rbnSandblastYes.Checked,
                Locks = txtLocks.Text.NullIfEmpty(),
                PaintColor = txtPaintColor.Text.NullIfEmpty(),
                RequiresNewSeals = rbnNewSeals.Checked,
                Seals = txtSeals.Text.NullIfEmpty(),
                SealsDeliveredToID = (cboSealsDeliveredTo.SelectedItem == null) ? new int?() : ((User)cboSealsDeliveredTo.SelectedItem).UserID,
                SealsDeliveredToName = (cboSealsDeliveredTo.SelectedItem == null) ? cboSealsDeliveredTo.Text : null,
                RequiresNewCapacitor = rbnCapacitorYes.Checked,
                CapacitorVoltage = txtCapacitorVoltage.Text.ParseInteger(),
                CapacitorCapacity = txtCapacitorCapacity.Text.ParseInteger(),
                RequiresShaftCladding = rbnShaftCladdingYes.Checked,
                RequiresShaftManufacturing = rbnShaftManufacturingYes.Checked,
                CableLenght = txtCableLenght.Text.ParseInteger(),
                CableSize = txtCableSize.Text.NullIfEmpty(),
                RequiresOrings = rbnOringsYes.Checked,
                RequiresJoints = rbnJointsYes.Checked,
                RequiresDielectricOil = rbnDielectricOilYes.Checked,
                RequiresNewImpeller = rbnImpellerYes.Checked,
                RequiresPins = rbnPinsYes.Checked,
                RequiresPackings = rbnPackingsYes.Checked,
                RequiresGrease = rbnGreaseYes.Checked,
                MissingParts = txtMissingParts.Text.NullIfEmpty(),
                Notes = txtNotes.Text.NullIfEmpty(),
                StorageLocation = txtStorageLocation.Text,
                Status = "Esperando desarme",
                PriorityID = 3,
                Approved = false,
                Completed = false,
                Stage = 0,
                AppliesForWinding = !rbnWindingNotApplies.Checked,
                SealsApply = !rbnSealsNotApply.Checked,
                DeliveryNoteNumber = txtDeliveryNoteNumber.Text.NullIfEmpty(),
                InvoiceNumber = txtInvoiceNumber.Text.NullIfEmpty()
            };
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        order.Insert(handler);
                        var progressUpdate = new ProgressUpdate()
                        {
                            RepairOrderID = order.RepairOrderID,
                            UserID = AppEnvironment.CurrentUser.UserID,
                            Date = DateTime.Now,
                            UpdateTypeID = 1,
                            Notes = "Registrado automáticamente."
                        };
                        progressUpdate.Insert(handler);
                        handler.CommitTransaction();
                    }
                });
                MessageBox.Show("Copia registrada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
            }
            catch (Exception dbException)
            {
                // Waypoint RO205
                MessageBox.Show("(RO205) Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO205 (Flag: MySql). Message: " + dbException.Message);
                RepairOrderID = null;
            }
        }
        private async void btnMakePDF_Click(object sender, EventArgs e)
        {
            string pdfPath;

            // Comprobación carpeta de destino.
            try
            {
                string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Ordenes de reparacion") :
                    Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, "Ordenes de reparacion");
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }
                pdfPath = Path.Combine(destinationFolder, $"Orden de reparacion {CurrentRepairOrder.RepairOrderID:D8}.pdf");
            }
            catch (Exception pdfExportException)
            {
                // Waypoint RO206
                MessageBox.Show("(RO206) Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO206. Message: " + pdfExportException.Message);
                return;
            }

            // Generación de orden de reparación PDF.
            try
            {
                await Task.Run(() => PdfGeneration.ExportPdfRepairOrder(CurrentRepairOrder, pdfPath));
            }
            catch (Exception pdfExportException)
            {
                // Waypoint RO207
                MessageBox.Show("(RO207) Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO207. Message: " + pdfExportException.Message);
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
                // Waypoint RO208
                MessageBox.Show("(RO208) Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO208. Message: " + pdfOpenException.Message);
            }
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtRefNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRepairOrderID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInvoiceNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
