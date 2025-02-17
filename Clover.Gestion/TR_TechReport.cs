using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class TR_TechReport : Form
    {
        private int? TechReportID = null;
        private TechReport CurrentReport = null;
        private bool SafeExit = false;

        public TR_TechReport(int? TechReportID = null)
        {
            this.TechReportID = TechReportID;
            InitializeComponent();
        }

        private async void TR_TechReport_Load(object sender, EventArgs e)
        {
            if (TechReportID.HasValue)
            {
                try
                {
                    CurrentReport = await Task.Run(() => TechReport.GetReportById(TechReportID.Value));
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    cboContact.DisplayMember = "ContactName";
                    cboContact.ValueMember = "ContactID";
                    cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(CurrentReport.CustomerID));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                }
                catch (Exception dbException)
                {
                    // Waypoint TR201
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint TR201 (Flag: MySql). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = "Visualizando informe: " + CurrentReport.TechReportID.ToString("D8");
                txtTechReportID.Text = CurrentReport.TechReportID.ToString("D8");
                dtpDate.Value = CurrentReport.Date;
                cboBusiness.SelectedValue = CurrentReport.BusinessID;
                cboCustomer.SelectedValue = CurrentReport.CustomerID;
                if (CurrentReport.ContactID.HasValue)
                {
                    cboContact.SelectedValue = CurrentReport.ContactID.Value;
                }
                else
                {
                    cboContact.SelectedItem = null;
                }
                htmlReportBody.SetDocumentHtml(CurrentReport.ReportBody);
                cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            }
            else
            {
                try
                {
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";
                    cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                    if (((List<Customer>)cboCustomer.DataSource).Count == 0)
                    {
                        MessageBox.Show("No hay clientes registrados en el sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SafeExit = true;
                        this.Close();
                        return;
                    }
                    var selectedCustomer = (Customer)cboCustomer.SelectedItem;
                    cboContact.DisplayMember = "ContactName";
                    cboContact.ValueMember = "ContactID";
                    cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(selectedCustomer.CustomerID));
                    cboBusiness.DisplayMember = "BusinessName";
                    cboBusiness.ValueMember = "BusinessID";
                    cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                }
                catch (Exception dbException)
                {
                    // Waypoint TR202
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint TR202 (Flag: MySql). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            }
        }
        private void TR_TechReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentReport == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }
        
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Guarda informe en base de datos.
            try
            {
                await SaveReport();
                this.Text = "Visualizando informe: " + CurrentReport.TechReportID.ToString("D8");
                txtTechReportID.Text = CurrentReport.TechReportID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint TR203
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR203 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            if (!CurrentReport.ContactID.HasValue)
            {
                MessageBox.Show("Informe guardado."
                    + Environment.NewLine + "No se generó informe PDF porque no hay contacto seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SafeExit = true;
                this.Close();
                return;
            }
            // Genera informe PDF.
            try
            {
                await ExportPdf();
            }
            catch (Exception exception)
            {
                // Waypoint TR204
                MessageBox.Show("Error al exportar informe PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR204. Message: " + exception.Message);
            }
            MessageBox.Show("Informe guardado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SafeExit = true;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }
        private async void btnMakePDF_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (cboContact.SelectedItem == null)
            {
                MessageBox.Show("No se puede exportar el informe porque no hay contacto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Guarda informe en base de datos.
            try
            {
                await SaveReport();
                this.Text = "Visualizando informe: " + CurrentReport.TechReportID.ToString("D8");
                txtTechReportID.Text = CurrentReport.TechReportID.ToString("D8");
            }
            catch (Exception dbException)
            {
                // Waypoint TR205
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR205 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Genera informe PDF.
            string pdfPath;
            try
            {
                pdfPath = await ExportPdf();
            }
            catch (Exception exception)
            {
                // Waypoint TR206
                MessageBox.Show("Error al exportar informe PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR206. Message: " + exception.Message);
                return;
            }
            // Abre archivo pdf.
            try
            {
                using (var pdfOpenProcess = new System.Diagnostics.Process())
                {
                    pdfOpenProcess.StartInfo.FileName = pdfPath;
                    pdfOpenProcess.Start();
                }
            }
            catch (Exception pdfOpenException)
            {
                // Waypoint TR207
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR207. Message: " + pdfOpenException.Message);
            }
        }

        private async void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCustomer = (Customer)cboCustomer.SelectedItem;
            cboBusiness.SelectedValue = selectedCustomer.BusinessID;
            try
            {
                cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(selectedCustomer.CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint TR208
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TR208 (Flag: MySql). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
            }
        }

        private async Task SaveReport()
        {
            var report = new TechReport()
            {
                TechReportID = (CurrentReport == null) ? 0 : CurrentReport.TechReportID,
                BusinessID = (int)cboBusiness.SelectedValue,
                CustomerID = (int)cboCustomer.SelectedValue,
                ContactID = (cboContact.SelectedItem == null) ? new int?() : (int)cboContact.SelectedValue,
                Date = dtpDate.Value.Date,
                ReportBody = htmlReportBody.GetDocumentHtml()
            };
            if (CurrentReport == null)
            {
                await Task.Run(() => report.Insert());
            }
            else
            {
                await Task.Run(() => report.Update());
            }
            CurrentReport = report;
        }
        private async Task<string> ExportPdf()
        {
            // Recupera información adicional.
            var selectedBusiness = await Task.Run(() => Business.GetBusinessById(CurrentReport.BusinessID));
            var selectedCustomer = await Task.Run(() => Customer.GetCustomerById(CurrentReport.CustomerID));
            var selectedContact = await Task.Run(() => CustomerContact.GetContactById(CurrentReport.ContactID.Value));
            // Comprobación carpeta de destino.
            string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Informes técnicos " + selectedBusiness.BusinessName) :
                Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, "Informes técnicos " + selectedBusiness.BusinessName);
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string pdfPath = Path.Combine(destinationFolder, $"Informe {CurrentReport.TechReportID:D8} - {selectedCustomer.CustomerName.RemoveIllegalCharacters()}.pdf");
            // Generación de informe PDF.
            await Task.Run(() => PdfGeneration.ExportPdfTechReport(CurrentReport, selectedBusiness, selectedCustomer, selectedContact, pdfPath));
            return pdfPath;
        }
    }
}
