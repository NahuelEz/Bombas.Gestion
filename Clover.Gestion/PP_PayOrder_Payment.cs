using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PP_PayOrder_Payment : Form
    {
        private PayOrderPayment CurrentPayment = null;
        
        public PP_PayOrder_Payment(PayOrderPayment Payment = null)
        {
            this.CurrentPayment = Payment;
            InitializeComponent();
        }

        private async void PP_PayOrder_Payment_Load(object sender, EventArgs e)
        {
            try
            {
                cboCurrency.DisplayMember = "CurrencyName";
                cboCurrency.ValueMember = "CurrencyID";
                cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                cboPayment.DisplayMember = "PaymentName";
                cboPayment.ValueMember = "PaymentID";
                cboPayment.DataSource = await Task.Run(() => Payment.GetPayments());
            }
            catch (Exception dbException)
            {
                // Waypoint PP401
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PP401 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            if (CurrentPayment != null)
            {
                cboPayment.SelectedValue = CurrentPayment.PaymentID;
                nudTotalAmount.Value = CurrentPayment.TotalAmount;
                cboCurrency.SelectedValue = CurrentPayment.CurrencyID;
                txtAdditionalInformation.Text = CurrentPayment.AdditionalInformation;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (nudTotalAmount.Value == 0)
            {
                MessageBox.Show("El importe debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CurrentPayment == null)
            {
                ((PP_PayOrder)(this.Owner)).Payments.Add(new PayOrderPayment()
                {
                    PaymentID = (int)cboPayment.SelectedValue,
                    TotalAmount = nudTotalAmount.Value,
                    CurrencyID = (int)cboCurrency.SelectedValue,
                    AdditionalInformation = txtAdditionalInformation.Text,
                    // Información adicional para visualización en detalle.
                    PaymentName = ((Payment)cboPayment.SelectedItem).PaymentName,
                    CurrencySymbol = ((Currency)cboCurrency.SelectedItem).CurrencySymbol
                });
            }
            else
            {
                CurrentPayment.PaymentID = (int)cboPayment.SelectedValue;
                CurrentPayment.TotalAmount = nudTotalAmount.Value;
                CurrentPayment.CurrencyID = (int)cboCurrency.SelectedValue;
                CurrentPayment.AdditionalInformation = txtAdditionalInformation.Text;
                // Información adicional para visualización en detalle.
                CurrentPayment.PaymentName = ((Payment)cboPayment.SelectedItem).PaymentName;
                CurrentPayment.CurrencySymbol = ((Currency)cboCurrency.SelectedItem).CurrencySymbol;
            }
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
