using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class RO_LinkEstimate : Form
    {
        private int RepairOrderID;
        private int CustomerID;

        public RO_LinkEstimate(int RepairOrderID, int CustomerID)
        {
            this.RepairOrderID = RepairOrderID;
            this.CustomerID = CustomerID;
            InitializeComponent();
        }

        private async void RO_LinkEstimate_Load(object sender, EventArgs e)
        {
            try
            {
                cboEstimate.DataSource = await Task.Run(() => Estimate.GetEstimatesByCustomerId(CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint RO501
                MessageBox.Show("(RO501) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO501 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }

        private void cboEstimate_Format(object sender, ListControlConvertEventArgs e)
        {
            var estimate = (Estimate)e.ListItem;
            e.Value = $"{estimate.EstimateID:D4} - Importe: {estimate.TotalBeforeTax:N2} {estimate.CurrencySymbol}";
        }
        
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            int selectedEstimateId = ((Estimate)cboEstimate.SelectedItem).EstimateID;
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        var linkedRepairOrder = RepairOrder.GetRepairOrderById(RepairOrderID, handler);
                        // Registra automáticamente el desarme si corresponde.
                        if (linkedRepairOrder.Stage == 0)
                        {
                            var update1 = new ProgressUpdate();
                            update1.RepairOrderID = linkedRepairOrder.RepairOrderID;
                            update1.UserID = AppEnvironment.CurrentUser.UserID;
                            update1.Date = DateTime.Now;
                            update1.UpdateTypeID = 2;
                            update1.Insert(handler);
                            linkedRepairOrder.Stage = 1;
                        }
                        // Registra actualización de progreso.
                        var update2 = new ProgressUpdate();
                        update2.RepairOrderID = linkedRepairOrder.RepairOrderID;
                        update2.UserID = AppEnvironment.CurrentUser.UserID;
                        update2.Date = DateTime.Now;
                        update2.UpdateTypeID = 12;
                        update2.Insert(handler);
                        // Actualiza orden de reparación.
                        linkedRepairOrder.EstimateID = selectedEstimateId;
                        if (linkedRepairOrder.Stage == 1)
                        {
                            // Si está en la etapa correcta (puede cotizarse también en etapa 2), además actualiza estado.
                            linkedRepairOrder.Status = "Esperando aprobación";
                        }
                        linkedRepairOrder.Update(handler);
                        handler.CommitTransaction();
                    }
                });
                MessageBox.Show("Presupuesto asociado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception dbException)
            {
                // Waypoint RO502
                MessageBox.Show("(RO502) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO502 (Flag: MySQL). Message: " + dbException.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
