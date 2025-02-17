using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_CurrencyConverter : Form
    {
        private const string ExchangeRatesFeed = @"https://www.invertironline.com/mercado/cotizaciones/argentina/monedas/principales-divisas";
        private decimal? USDExchangeRate = null;
        private Currency CurrentCurrency;

        public int DestinationCurrencyID;
        public decimal ExchangeRate;

        public SA_CurrencyConverter(Currency CurrentCurrency)
        {
            this.CurrentCurrency = CurrentCurrency;
            InitializeComponent();
        }

        private async void ES_CurrencyConverter_Load(object sender, EventArgs e)
        {
            try
            {
                var currencies = await Task.Run(() => Currency.GetCurrencies());
                var availableCurrencies = currencies.Where(x => x.CurrencyID != CurrentCurrency.CurrencyID).ToList();
                cboDestinationCurrency.DisplayMember = "CurrencyName";
                cboDestinationCurrency.ValueMember = "CurrencyID";
                cboDestinationCurrency.DataSource = availableCurrencies;
            }
            catch (Exception dbException)
            {
                // Waypoint SA601
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA601 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            txtCurrency.Text = CurrentCurrency.CurrencyName;
            lblCurrencySymbol.Text = $"1 {CurrentCurrency.CurrencySymbol} =";
            lblDestinationCurrencySymbol.Text = ((Currency)cboDestinationCurrency.SelectedItem).CurrencySymbol;
            // Descarga cotización del dolar
            try
            {
                using (var webClient = new WebClient())
                {
                    string result = await webClient.DownloadStringTaskAsync(ExchangeRatesFeed);
                    int startIndex = result.IndexOf("Dolar Banco");
                    var regex = new Regex("<td[^>]*>([^<]*)</td>");
                    var matches = regex.Matches(result, startIndex).Cast<Match>().Take(2).Select(m => m.Groups[1].Value).ToArray();
                    USDExchangeRate = decimal.Parse(matches[1]);
                }
            }
            catch (Exception exception)
            {
                // Waypoint SA602
                MessageBox.Show("Error al solicitar cotización de Dolar Estadounidense (USD)."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA602. Message: " + exception.Message);
                return;
            }
            if (CurrentCurrency.CurrencySymbol == "USD" && ((Currency)cboDestinationCurrency.SelectedItem).CurrencySymbol == "ARS")
            {
                nudExchangeRate.Value = USDExchangeRate.Value;
            }
            else if (CurrentCurrency.CurrencySymbol == "ARS" && ((Currency)cboDestinationCurrency.SelectedItem).CurrencySymbol == "USD")
            {
                nudExchangeRate.Value = (1 / USDExchangeRate.Value);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DestinationCurrencyID = (int)cboDestinationCurrency.SelectedValue;
            ExchangeRate = nudExchangeRate.Value;
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDestinationCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDestinationCurrencySymbol.Text = ((Currency)cboDestinationCurrency.SelectedItem).CurrencySymbol;
            if (USDExchangeRate.HasValue)
            {
                if (CurrentCurrency.CurrencySymbol == "USD" && ((Currency)cboDestinationCurrency.SelectedItem).CurrencySymbol == "ARS")
                {
                    nudExchangeRate.Value = USDExchangeRate.Value;
                }
                else if (CurrentCurrency.CurrencySymbol == "ARS" && ((Currency)cboDestinationCurrency.SelectedItem).CurrencySymbol == "USD")
                {
                    nudExchangeRate.Value = (1 / USDExchangeRate.Value);
                }
            }
        }
    }
}
