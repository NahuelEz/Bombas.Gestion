using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class RO_OrderUpdate : Form
    {
        private int RepairOrderID;
        private int? ProgressUpdateID = null;
        private ProgressUpdate CurrentProgressUpdate = null;

        public RO_OrderUpdate(int RepairOrderID, int? ProgressUpdateID = null)
        {
            this.RepairOrderID = RepairOrderID;
            this.ProgressUpdateID = ProgressUpdateID;
            InitializeComponent();
        }

        private async void RO_OrderUpdate_Load(object sender, EventArgs e)
        {
            if (ProgressUpdateID.HasValue)
            {
                try
                {
                    CurrentProgressUpdate = await Task.Run(() => ProgressUpdate.GetUpdateById(ProgressUpdateID.Value));
                }
                catch (Exception dbException)
                {
                    // Waypoint RO401
                    MessageBox.Show("(RO401) Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RO401 (Flag: MySQL). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                txtUserName.Text = CurrentProgressUpdate.UserName;
                txtDate.Text = CurrentProgressUpdate.Date.ToString("dd/MM/yy HH:mm");
                cboUpdateType.DropDownStyle = ComboBoxStyle.Simple;
                cboUpdateType.Enabled = false;
                cboUpdateType.Text = CurrentProgressUpdate.UpdateTypeName;
                txtNotes.Text = CurrentProgressUpdate.Notes;
                txtNotes.ReadOnly = true;
                btnAccept.Enabled = false;
            }
            else
            {
                try
                {
                    var currentRepairOrder = await Task.Run(() => RepairOrder.GetRepairOrderById(RepairOrderID));
                    if (currentRepairOrder.Completed)
                    {
                        MessageBox.Show("No se puede actualizar la orden de reparación porque la misma ya fue completada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }
                    cboUpdateType.DisplayMember = "UpdateTypeName";
                    cboUpdateType.ValueMember = "UpdateTypeID";
                    cboUpdateType.DataSource = await Task.Run(() => UpdateType.GetAllowedUpdateTypes(AppEnvironment.CurrentUser.AccessLevel, currentRepairOrder.RepairOrderID, currentRepairOrder.Stage));
                }
                catch (Exception dbException)
                {
                    // Waypoint RO402
                    MessageBox.Show("(RO402) Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint RO402 (Flag: MySQL). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                txtUserName.Text = AppEnvironment.CurrentUser.UserName;
                txtDate.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones
            if ((int)cboUpdateType.SelectedValue == 6 && string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                MessageBox.Show("Por favor, complete el N° de bobinado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var prompt = MessageBox.Show("Por favor, confirme la operación.", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (prompt != DialogResult.OK)
            {
                return;
            }
            // Genera objecto "progress_update"
            int updateTypeId = (int)cboUpdateType.SelectedValue;
            var update = new ProgressUpdate()
            {
                RepairOrderID = this.RepairOrderID,
                UserID = AppEnvironment.CurrentUser.UserID,
                Date = DateTime.Now,
                UpdateTypeID = updateTypeId,
                Notes = txtNotes.Text.NullIfEmpty()
            };
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        update.Insert(handler);
                        // Para determinadas actualizaciones, actualiza estado de la orden de reparación.
                        switch (updateTypeId)
                        {
                            case 11: // Listo para entregar
                                {
                                    RepairOrder.SetAsCompleted(RepairOrderID, handler);
                                    RepairOrder.UpdateStatusById(RepairOrderID, "Finalizado", handler);
                                    break;
                                }
                            case 12: // Cotizado
                                {
                                    RepairOrder.UpdateStatusById(RepairOrderID, "Esperando aprobación", handler);
                                    break;
                                }
                            case 13: // Aprobado
                                {
                                    RepairOrder.SetAsApproved(RepairOrderID, handler);
                                    RepairOrder.UpdateStageById(RepairOrderID, 2, handler);
                                    RepairOrder.UpdateStatusById(RepairOrderID, "En curso", handler);
                                    break;
                                }
                            case 14: // Rechazado
                                {
                                    RepairOrder.SetAsCompleted(RepairOrderID, handler);
                                    RepairOrder.UpdateStageById(RepairOrderID, 2, handler);
                                    RepairOrder.UpdateStatusById(RepairOrderID, "Rechazado", handler);
                                    break;
                                }
                            case 2: // Desarme
                                {
                                    RepairOrder.UpdateStageById(RepairOrderID, 1, handler);
                                    RepairOrder.UpdateStatusById(RepairOrderID, "Esperando cotización", handler);
                                    break;
                                }
                        }
                        handler.CommitTransaction();
                    }
                });
                MessageBox.Show("Actualización registrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception dbException)
            {
                // Waypoint RO403
                MessageBox.Show("(RO403) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO403 (Flag: MySQL). Message: " + dbException.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboUpdateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblWindingWarning.Visible = ((int)cboUpdateType.SelectedValue == 6);    // ID 6 : Bobinado
        }
    }
}
