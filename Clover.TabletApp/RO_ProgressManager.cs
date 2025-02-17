using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.TabletApp
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.A):
                    {
                        btnUpdate.PerformClick();
                        return true;
                    }
                case Keys.Escape:
                    {
                        btnCancel.PerformClick();
                        return true;
                    }
                case Keys.Enter:
                    {
                        btnOpenUpdate.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void btnOpenUpdate_Click(object sender, EventArgs e)
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

        private async Task UpdateProgressUpdatesAsync()
        {
            try
            {
                dgvProgressUpdates.DataSource = await Task.Run(() => ProgressUpdate.GetUpdatesByRepairOrderId(RepairOrderID));
            }
            catch (Exception dbException)
            {
                // Waypoint PM001
                MessageBox.Show("(PM001) Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PM001 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
            }
        }
    }
}
