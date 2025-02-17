using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class IN_InputHistory : Form
    {
        private int InputID;

        public IN_InputHistory(int InputID)
        {
            this.InputID = InputID;
            InitializeComponent();
            dgvHistory.AutoGenerateColumns = false;
        }

        private async void IN_InputHistory_Load(object sender, EventArgs e)
        {
            try
            {
                dgvHistory.DataSource = await Task.Run(() => PurchaseOrderItem.GetItemsByInputId(InputID));
                if (((List<PurchaseOrderItem>)dgvHistory.DataSource).Count == 0)
                {
                    MessageBox.Show("No se encontraron cotizaciones para este insumo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception dbException)
            {
                // Waypoint IN701
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint IN701 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
