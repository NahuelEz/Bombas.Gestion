using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_DeliveryNote : Form
    {
        private int? SaleID = null;
        private int? DeliveryNoteID = null;
        private DeliveryNote CurrentDeliveryNote = null;
        
        public SA_DeliveryNote(int parameter, DNParameterType parameterType)
        {
            switch (parameterType)
            {
                case DNParameterType.SaleID:
                    {
                        SaleID = parameter;
                        break;
                    }
                case DNParameterType.DeliveryNoteID:
                    {
                        DeliveryNoteID = parameter;
                        break;
                    }
            }
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
        }

        private async void SA_DeliveryNote_Load(object sender, EventArgs e)
        {
            if (DeliveryNoteID.HasValue)
            {
                try
                {
                    CurrentDeliveryNote = await Task.Run(() => DeliveryNote.GetDeliveryNoteById(DeliveryNoteID.Value));
                    cboInvoiceType.DataSource = new string[] { "A", "B", "C" };
                    dgvItems.DataSource = await Task.Run(() => SaleItem.GetItemsByDeliveryNoteId(DeliveryNoteID.Value));
                }
                catch (Exception dbException)
                {
                    // Waypoint SA401
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SA401 (Flag: MySQL). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                // Verifica si se trata de un remito sin número.
                if (CurrentDeliveryNote.Number == "S/N")
                {
                    chkUnregistered.Checked = true;
                }
                else
                {
                    txtDeliveryNoteNumber.Text = CurrentDeliveryNote.Number;
                    cboInvoiceType.SelectedItem = CurrentDeliveryNote.InvoiceType;
                    txtInvoiceNumber.Text = CurrentDeliveryNote.MaskedInvoiceNumber;
                }
                // Coloca tilde a todos los ítems.
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    row.Cells["selectionColumn"].Value = true;
                }
            }
            else
            {
                try
                {
                    cboInvoiceType.DataSource = new string[] { "A", "B", "C" };
                    dgvItems.DataSource = await Task.Run(() => SaleItem.GetItemsBySaleId(SaleID.Value));
                }
                catch (Exception dbException)
                {
                    // Waypoint SA402
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint SA402 (Flag: MySQL). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                // Deshabilita la selección de los ítems que ya tienen remito asociado.
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    if (((SaleItem)row.DataBoundItem).DeliveryNoteID.HasValue)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        ((DataGridViewCheckBoxCell)row.Cells["selectionColumn"]).ReadOnly = true;
                    }
                }
                // Si todos los ítems están asociados, deshabilita el botón "Aceptar".
                if (((List<SaleItem>)dgvItems.DataSource).All(X => X.DeliveryNoteID.HasValue))
                {
                    btnAccept.Enabled = false;
                    MessageBox.Show("Todos los ítems se encuentran asociados a un remito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            var selectedItems = dgvItems.Rows
                .Cast<DataGridViewRow>()
                .Where(X => Convert.ToBoolean(((DataGridViewCheckBoxCell)X.Cells["selectionColumn"]).EditedFormattedValue))
                .Select(X => (SaleItem)X.DataBoundItem)
                .ToList();
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtDeliveryNoteNumber.Text) && chkUnregistered.Checked == false)
            {
                MessageBox.Show("El N° de remito no puede estar vacío.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un ítem para asociar al remito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Construye objeto principal
            var deliveryNote = new DeliveryNote()
            {
                DeliveryNoteID = (CurrentDeliveryNote == null) ? 0 : CurrentDeliveryNote.DeliveryNoteID,
                SaleID = (CurrentDeliveryNote == null) ? SaleID.Value : CurrentDeliveryNote.SaleID,
                Number = (chkUnregistered.Checked) ? "S/N" : txtDeliveryNoteNumber.Text.Trim(),
                PrintingDate = DateTime.Today,
                InvoiceType = string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) ? null : (string)cboInvoiceType.SelectedItem,
                MaskedInvoiceNumber = string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) ? null : txtInvoiceNumber.Text.Trim()
            };
            bool unregistered = chkUnregistered.Checked;
            // Actualiza base de datos
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        if (CurrentDeliveryNote == null)
                        {
                            // Registra remito.
                            deliveryNote.Insert(handler);
                        }
                        else
                        {
                            // Actualiza remito.
                            deliveryNote.Update(handler);
                            // "Des-linkea" todos los ítems anteriores asociados al remito.
                            SaleItem.UnlinkAllItemsFromDeliveryNote(deliveryNote.DeliveryNoteID, handler);
                        }
                        // "Linkea" los ítems correspondientes al remito.
                        foreach (var item in selectedItems)
                        {
                            SaleItem.LinkItemToDeliveryNote(deliveryNote.DeliveryNoteID, item.ItemID, handler);
                        }
                        if (!unregistered)
                        {
                            // Actualiza orden de reparación (si corresponde)
                            RepairOrder.UpdateInvoiceNumberBySaleId(deliveryNote.SaleID, deliveryNote.MaskedInvoiceNumber, handler);
                            RepairOrder.UpdateDeliveryNoteNumberBySaleId(deliveryNote.SaleID, deliveryNote.Number, handler);
                        }
                        handler.CommitTransaction();
                    }
                });
            }
            catch (Exception dbException)
            {
                // Waypoint SA403
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA403 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            // Solicita información adicional.
            Sale sale;
            Customer customer;
            try
            {
                sale = await Task.Run(() => Sale.GetSaleById(deliveryNote.SaleID));
                customer = await Task.Run(() => Customer.GetCustomerById(sale.CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint SA404
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA404 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
                return;
            }
            // Exporta remito PDF.
            string pdfPath = string.Empty;
            try
            {
                // Comprobación carpeta de destino.
                string destinationFolder = (string.IsNullOrWhiteSpace(AppEnvironment.CurrentSettings.PdfDocumentsFolder)) ?
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Remitos {sale.BusinessName}") :
                    Path.Combine(AppEnvironment.CurrentSettings.PdfDocumentsFolder, $"Remitos {sale.BusinessName}");
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }
                if (unregistered)
                {
                    pdfPath = Path.Combine(destinationFolder, $"{customer.CustomerName.RemoveIllegalCharacters()}_{DateTime.Now:ddMMyyHHmmss}.pdf");
                }
                else
                {
                    pdfPath = Path.Combine(destinationFolder, $"{deliveryNote.Number.RemoveIllegalCharacters()} - {customer.CustomerName.RemoveIllegalCharacters()}.pdf");
                }
                // Generación de comprobante PDF.
                await Task.Run(() => PdfGeneration.ExportPdfDeliveryNote(deliveryNote, selectedItems, sale, customer, pdfPath, unregistered));
            }
            catch (Exception pdfExportException)
            {
                // Waypoint SA405
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA405. Message: " + pdfExportException.Message);
                this.Close();
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
                // Waypoint SA406
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA406. Message: " + pdfOpenException.Message);
                this.Close();
                return;
            }
            // Pregunta al usuario para generar factura correspondiente.
            if (CurrentDeliveryNote == null && !unregistered)
            {
                var prompt = MessageBox.Show("¿Desea registrar la factura correspondiente?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (prompt == DialogResult.Yes)
                {
                    using (var form = new SI_SaleInvoice(deliveryNote.DeliveryNoteID, SIParameterType.DeliveryNoteID))
                    {
                        form.ShowDialog();
                    }
                }
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
        private void chkUnregistered_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnregistered.Checked)
            {
                txtDeliveryNoteNumber.Text = string.Empty;
                txtInvoiceNumber.Text = string.Empty;
                txtDeliveryNoteNumber.Enabled = false;
                cboInvoiceType.Enabled = false;
                txtInvoiceNumber.Enabled = false;
            }
            else
            {
                txtDeliveryNoteNumber.Enabled = true;
                cboInvoiceType.Enabled = true;
                txtInvoiceNumber.Enabled = true;
            }
        }
    }

    public enum DNParameterType { SaleID, DeliveryNoteID }
}
