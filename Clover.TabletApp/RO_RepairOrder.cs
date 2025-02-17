using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.TabletApp
{
    public partial class RO_RepairOrder : Form
    {
        private int? RepairOrderID = null;
        private RepairOrder CurrentRepairOrder = null;
        private bool SafeExit = false;
        private bool sqlConnectionBusy = false;
        private Image DevicePicture = null;

        public RO_RepairOrder(int? RepairOrderID = null)
        {
            this.RepairOrderID = RepairOrderID;
            InitializeComponent();
        }

        private async void RO_RepairOrder_Load(object sender, EventArgs e)
        {
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
                    // Waypoint RP001
                    MessageBox.Show("(RP001) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RP001 (Flag: MySql). Message: " + dbException.Message);
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
                    // Waypoint RP002
                    MessageBox.Show("(RP002) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RP002 (Flag: MySql). Message: " + dbException.Message);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((cboCustomer.Focused && cboCustomer.DroppedDown)
                || (cboMotorType.Focused && cboMotorType.DroppedDown)
                || (cboSealsDeliveredTo.Focused && cboSealsDeliveredTo.DroppedDown))
            {
                // Built-in key handling
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {
                // Custom key handling
                switch (keyData)
                {
                    case Keys.Up:
                        {
                            this.SelectNextControl(this.ActiveControl, false, true, true, false);
                            return true;
                        }
                    case Keys.Down:
                        {
                            this.SelectNextControl(this.ActiveControl, true, true, true, false);
                            return true;
                        }
                    case Keys.Enter:
                        {
                            if (cboCustomer.Focused)
                            {
                                cboCustomer.DroppedDown = true;
                            }
                            else if (cboMotorType.Focused)
                            {
                                cboMotorType.DroppedDown = true;
                            }
                            else if (cboSealsDeliveredTo.Focused)
                            {
                                cboSealsDeliveredTo.DroppedDown = true;
                            }
                            else
                            {
                                btnAccept.PerformClick();
                            }
                            return true;
                        }
                    case Keys.Escape:
                        {
                            btnCancel.PerformClick();
                            return true;
                        }
                    case (Keys.Control | Keys.P):
                        {
                            btnProgressManager.PerformClick();
                            return true;
                        }
                    default:
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                }
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.LightYellow;
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
        }
        private void panel_Enter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.LightYellow;
        }
        private void panel_Leave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Control;
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            if (!(AppEnvironment.CurrentUser.AccessLevel >= 3))
            {
                MessageBox.Show("El usuario actual no tiene los permisos necesarios para realizar esta operación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sqlConnectionBusy)
            {
                MessageBox.Show("Por favor, espere mientras se completa la operación actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CurrentRepairOrder != null && (CurrentRepairOrder.EstimateID.HasValue || CurrentRepairOrder.Approved || CurrentRepairOrder.Completed))
            {
                MessageBox.Show("La orden de reparación no puede ser modificada porque ya fue cotizada, aprobada o completada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Mandatory fields check
            bool mandatoryCheckPassed = true;
            if (string.IsNullOrWhiteSpace(cboCustomer.Text))
            {
                cboCustomer.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (cboCustomer.SelectedItem == null && string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                txtPhoneNumber.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (string.IsNullOrWhiteSpace(cboMotorType.Text))
            {
                cboMotorType.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (string.IsNullOrWhiteSpace(txtPumpBrand.Text))
            {
                txtPumpBrand.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (string.IsNullOrWhiteSpace(txtPumpModel.Text))
            {
                txtPumpModel.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (rbnCapacitorYes.Checked && string.IsNullOrWhiteSpace(txtCapacitorVoltage.Text))
            {
                txtCapacitorVoltage.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (rbnCapacitorYes.Checked && string.IsNullOrWhiteSpace(txtCapacitorCapacity.Text))
            {
                txtCapacitorCapacity.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (string.IsNullOrWhiteSpace(txtStorageLocation.Text))
            {
                txtStorageLocation.BackColor = Color.FromArgb(230, 185, 185);
                mandatoryCheckPassed = false;
            }
            if (!mandatoryCheckPassed)
            {
                MessageBox.Show("Uno o mas campos obligatorios están incompletos. Los campos obligatorios están indicados con un asterisco (*)."
                    + Environment.NewLine + Environment.NewLine + "El campo Teléfono también es obligatorio si el cliente especificado no está registrado."
                    + Environment.NewLine + "Los campos Voltaje y Capacitancia también son obligatorios si se debe reemplazar capacitor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Numeric fields format check
            bool formatCheckPassed = true;
            if (!string.IsNullOrWhiteSpace(txtCapacitorVoltage.Text) && !int.TryParse(txtCapacitorVoltage.Text, out int result1))
            {
                txtCapacitorVoltage.BackColor = Color.FromArgb(230, 185, 185);
                formatCheckPassed = false;
            }
            if (!string.IsNullOrWhiteSpace(txtCapacitorCapacity.Text) && !int.TryParse(txtCapacitorCapacity.Text, out int result2))
            {
                txtCapacitorCapacity.BackColor = Color.FromArgb(230, 185, 185);
                formatCheckPassed = false;
            }
            if (!string.IsNullOrWhiteSpace(txtCableLenght.Text) && !int.TryParse(txtCableLenght.Text, out int result3))
            {
                txtCableLenght.BackColor = Color.FromArgb(230, 185, 185);
                formatCheckPassed = false;
            }
            if (!formatCheckPassed)
            {
                MessageBox.Show("Uno o mas campos numéricos no tienen el formato correcto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Construye objeto "repair_order".
            var order = new RepairOrder()
            {
                RepairOrderID = (CurrentRepairOrder == null) ? 0 : CurrentRepairOrder.RepairOrderID,
                RefNumber = txtRefNumber.Text.NullIfEmpty(),
                Date = dtpDate.Value,
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
                DeliveryNoteNumber = (CurrentRepairOrder == null) ? null : CurrentRepairOrder.DeliveryNoteNumber,
                InvoiceNumber = (CurrentRepairOrder == null) ? null : CurrentRepairOrder.InvoiceNumber,
                ApprovalDate = (CurrentRepairOrder == null) ? null : CurrentRepairOrder.ApprovalDate,
                EstimateID = (CurrentRepairOrder == null) ? null : CurrentRepairOrder.EstimateID,
                DevicePicture = DevicePicture.ToByteArray()
            };
            if (CurrentRepairOrder == null)
            {
                try
                {
                    sqlConnectionBusy = true;
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
                    // Waypoint RP003
                    MessageBox.Show("(RP003) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RP003 (Flag: MySql). Message: " + dbException.Message);
                    RepairOrderID = null;
                }
                finally
                {
                    sqlConnectionBusy = false;
                }
            }
            else
            {
                try
                {
                    sqlConnectionBusy = true;
                    await Task.Run(() => order.Update());
                    MessageBox.Show("Orden de reparación actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint RP004
                    MessageBox.Show("(RP004) Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RP004 (Flag: MySql). Message: " + dbException.Message);
                }
                finally
                {
                    sqlConnectionBusy = false;
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void btnTakePicture_Click(object sender, EventArgs e)
        {
            using (var form = new CameraUtility())
            {
                form.ShowDialog();
                if (form.CapturedImage != null)
                {
                    DevicePicture = form.CapturedImage;
                }
            }
        }
    }
}
