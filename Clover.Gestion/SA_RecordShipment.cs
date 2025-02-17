using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_RecordShipment : Form
    {
        private int SaleID;

        public SA_RecordShipment(int SaleID)
        {
            this.SaleID = SaleID;
            InitializeComponent();
        }

        private async void SH_RecordShipment_Load(object sender, EventArgs e)
        {
            try
            {
                cboShippingCarrier.DisplayMember = "CarrierName";
                cboShippingCarrier.ValueMember = "ShippingCarrierID";
                cboShippingCarrier.DataSource = await Task.Run(() => ShippingCarrier.GetCarriers());
            }
            catch (Exception dbException)
            {
                // Waypoint SH301
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SH301 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            int selectedCarrierId = (int)cboShippingCarrier.SelectedValue;
            try
            {
                await Task.Run(() => Sale.UpdateShippingInformation(SaleID, true, DateTime.Today, selectedCarrierId));
                this.Close();
            }
            catch (Exception dbException)
            {
                // Waypoint SH302
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SH302 (Flag: MySQL). Message: " + dbException.Message);
            }
        }
    }
}
