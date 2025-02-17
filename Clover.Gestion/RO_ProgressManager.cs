using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class RO_ProgressManager : Form
    {
        private int RepairOrderID;

        public RO_ProgressManager(int RepairOrderID)
        {
            this.RepairOrderID = RepairOrderID;
            InitializeComponent();
            dgvProgressUpdates.AutoGenerateColumns = false;
        }

        private async void RO_ProgressManager_Load(object sender, EventArgs e)
        {
            await UpdateProgressUpdatesAsync();
        }
        
        private void dgvProgressUpdates_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona automáticamente la fila cuando se hace click con el botón derecho.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvProgressUpdates.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvProgressUpdates.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var form = new RO_OrderUpdate(RepairOrderID))
            {
                form.ShowDialog();
            }
            await UpdateProgressUpdatesAsync();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsItemOpenOrderUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProgressUpdates.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedProgressUpdate = (ProgressUpdate)dgvProgressUpdates.SelectedRows[0].DataBoundItem;
            using (var form = new RO_OrderUpdate(RepairOrderID, selectedProgressUpdate.ProgressUpdateID))
            {
                form.ShowDialog();
            }
        }

        private async Task UpdateProgressUpdatesAsync()
        {
            try
            {
                dgvProgressUpdates.DataSource = await Task.Run(() => ProgressUpdate.GetUpdatesByRepairOrderId(RepairOrderID));
            }
            catch (Exception dbException)
            {
                // Waypoint RO301
                MessageBox.Show("(RO301) Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint RO301 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
            }
        }
    }
}
