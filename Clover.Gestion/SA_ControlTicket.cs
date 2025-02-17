using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_ControlTicket : Form
    {
        private int SaleID;

        public SA_ControlTicket(int SaleID)
        {
            this.SaleID = SaleID;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
        }

        private async void SA_ControlTicket_Load(object sender, EventArgs e)
        {
            try
            {
                dgvItems.DataSource = await Task.Run(() => SaleItem.GetItemsBySaleId(SaleID));
            }
            catch (Exception dbException)
            {
                // Waypoint ES601
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES601 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }
        
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            var selectedItems = dgvItems.Rows
                .Cast<DataGridViewRow>()
                .Where(x => Convert.ToBoolean(((DataGridViewCheckBoxCell)x.Cells["selectionColumn"]).EditedFormattedValue))
                .Select(x => (SaleItem)x.DataBoundItem)
                .ToList();
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un ítem para incluir en el comprobante.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Solicitando información adicional
            Sale sale = null;
            Customer customer = null;
            try
            {
                sale = await Task.Run(() => Sale.GetSaleById(SaleID));
                customer = await Task.Run(() => Customer.GetCustomerById(sale.CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint ES602
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES602 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Exporta comprobante PDF.
            string pdfPath = string.Empty;
            try
            {
                // Comprobación carpeta de destino.
                string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Comprobantes X") :
                    Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, "Comprobantes X");
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }
                pdfPath = Path.Combine(destinationFolder, $"Comprobante venta {sale.SaleID:D8} - {customer.CustomerName.RemoveIllegalCharacters()}.pdf");
                // Generación de comprobante PDF.
                await Task.Run(() => PdfGeneration.ExportPdfControlTicket(sale, selectedItems, customer, pdfPath));
            }
            catch (Exception pdfExportException)
            {
                // Waypoint ES603
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES603. Message: " + pdfExportException.Message);
                return;
            }
            // Abre archivo PDF.
            try
            {
                using (var pdfOpenProcess = new Process())
                {
                    pdfOpenProcess.StartInfo.FileName = pdfPath;
                    pdfOpenProcess.Start();
                }
            }
            catch (Exception pdfOpenException)
            {
                // Waypoint ES604
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES604. Message: " + pdfOpenException.Message);
                return;
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            dgvItems.ClearSelection();
        }
    }
}
