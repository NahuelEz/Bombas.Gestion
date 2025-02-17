using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PV_CurrentAccount : Form
    {
        int ProviderID;
        string ProviderName;

        public PV_CurrentAccount(int ProviderID, string ProviderName)
        {
            this.ProviderID = ProviderID;
            this.ProviderName = ProviderName;
            InitializeComponent();
            dgvRecords.AutoGenerateColumns = false;
        }

        private async void PV_CurrentAccount_Load(object sender, EventArgs e)
        {
            List<PurchaseInvoice> invoices;
            List<PayOrderPayment> payments;
            try
            {
                cboCurrency.DisplayMember = "CurrencyName";
                cboCurrency.ValueMember = "CurrencyID";
                cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                int selectedCurrencyId = (int)cboCurrency.SelectedValue;
                invoices = await Task.Run(() => PurchaseInvoice.GetInvoicesByProviderId(ProviderID, selectedCurrencyId));
                payments = await Task.Run(() => PayOrderPayment.GetPaymentsByProviderId(ProviderID, selectedCurrencyId));
            }
            catch (Exception dbException)
            {
                // Waypoint PV501
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV501 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            // Concatena ambas secuencias.
            var dateSelector = new Func<DbEntity, DateTime>((param) =>
            {
                if (param is PurchaseInvoice)
                {
                    return ((PurchaseInvoice)param).InvoiceDate;
                }
                else
                {
                    return ((PayOrderPayment)param).Date;
                }
            });
            var records = invoices.Cast<DbEntity>().Concat(payments.Cast<DbEntity>()).OrderByDescending(dateSelector).ToList();
            // Carga información en interfaz.
            lblProviderName.Text = $"{ProviderID} - {ProviderName}";
            dgvRecords.DataSource = records;
            txtBalance.Text = (invoices.Sum(x => x.TotalAmount) - payments.Sum(x => x.TotalAmount)).ToString("N2");
            cboCurrency.SelectedIndexChanged += cboCurrency_SelectedIndexChanged;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void btnBuildPdf_Click(object sender, EventArgs e)
        {
            // Pregunta al usuario ruta de destino.
            string filePath;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.AddExtension = true;
                saveDialog.FileName = "informe.pdf";
                saveDialog.Filter = "Documento PDF|*.pdf";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            // Construye tabla a exportar.
            float[] columnWidths = { 1, 1, 1, 1 };
            string documentTitle = $"{ProviderName} - Estado de cuenta al {DateTime.Today:dd/MM/yyyy}\nDetalle de las últimas 20 operaciones registradas:";
            string footer = $"Saldo global: {txtBalance.Text}\nMontos expresados en: {((Currency)cboCurrency.SelectedItem).CurrencyName}";
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn() { ColumnName = "column1", Caption = "Fecha", DataType = typeof(DateTime) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "column2", Caption = "Factura N°", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "column3", Caption = "Orden de pago N°", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "column4", Caption = "Importe", DataType = typeof(decimal) });
            foreach (var record in ((List<DbEntity>)dgvRecords.DataSource).Take(20))
            {
                if (record is PurchaseInvoice)
                {
                    var invoice = (PurchaseInvoice)record;
                    dataTable.Rows.Add(invoice.InvoiceDate, invoice.InvoiceNumber, string.Empty, invoice.TotalAmount);
                }
                else
                {
                    var payment = (PayOrderPayment)record;
                    dataTable.Rows.Add(payment.Date, string.Empty, payment.PayOrderID.ToString("D8"), payment.TotalAmount);
                }
            }
            // Exporta información.
            try
            {
                await Task.Run(() => PdfGeneration.ExportPdfDataTable(documentTitle, columnWidths, dataTable, filePath, true, false, footer));
            }
            catch (Exception pdfExportException)
            {
                // Waypoint PV502
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV502. Message: " + pdfExportException.Message);
                return;
            }
            // Abre el archivo.
            try
            {
                using (var fileOpenProcess = new Process())
                {
                    fileOpenProcess.StartInfo.FileName = filePath;
                    fileOpenProcess.Start();
                }
            }
            catch (Exception pdfOpenException)
            {
                // Waypoint PV503
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV503. Message: " + pdfOpenException.Message);
            }
        }
        
        private void dgvRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                object entity = dgvRecords.Rows[e.RowIndex].DataBoundItem;
                if (entity is PurchaseInvoice)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((PurchaseInvoice)entity).InvoiceDate;
                            break;
                        case 1:
                            e.Value = ((PurchaseInvoice)entity).PurchaseInvoiceID;
                            break;
                        case 3:
                            e.Value = ((PurchaseInvoice)entity).TotalAmount;
                            break;
                        case 4:
                            e.Value = ((PurchaseInvoice)entity).CurrencySymbol;
                            break;
                    }
                }
                else
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            e.Value = ((PayOrderPayment)entity).Date;
                            break;
                        case 2:
                            e.Value = ((PayOrderPayment)entity).PayOrderID;
                            break;
                        case 3:
                            e.Value = ((PayOrderPayment)entity).TotalAmount;
                            break;
                        case 4:
                            e.Value = ((PayOrderPayment)entity).CurrencySymbol;
                            break;
                    }
                }
            }
        }
        private async void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PurchaseInvoice> invoices;
            List<PayOrderPayment> payments;
            try
            {
                int selectedCurrencyId = (int)cboCurrency.SelectedValue;
                invoices = await Task.Run(() => PurchaseInvoice.GetInvoicesByProviderId(ProviderID, selectedCurrencyId));
                payments = await Task.Run(() => PayOrderPayment.GetPaymentsByProviderId(ProviderID, selectedCurrencyId));
            }
            catch (Exception dbException)
            {
                // Waypoint PV504
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV504 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            // Concatena ambas secuencias.
            var dateSelector = new Func<DbEntity, DateTime>((param) =>
            {
                if (param is PurchaseInvoice)
                {
                    return ((PurchaseInvoice)param).InvoiceDate;
                }
                else
                {
                    return ((PayOrderPayment)param).Date;
                }
            });
            var records = invoices.Cast<DbEntity>().Concat(payments.Cast<DbEntity>()).OrderByDescending(dateSelector).ToList();
            // Carga información en interfaz.
            dgvRecords.DataSource = records;
            txtBalance.Text = (invoices.Sum(x => x.TotalAmount) - payments.Sum(x => x.TotalAmount)).ToString("N2");
        }
    }
}
