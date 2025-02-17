using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_InvoiceManager : Form
    {
        private int SaleID;

        public SA_InvoiceManager(int SaleID)
        {
            this.SaleID = SaleID;
            InitializeComponent();
            dgvInvoices.AutoGenerateColumns = false;
        }

        private async void SA_InvoiceManager_Load(object sender, EventArgs e)
        {
            await UpdateInvoicesAsync();
        }

        private async void btnAddInvoice_Click(object sender, EventArgs e)
        {
            using (var form = new SI_SaleInvoice(SaleID, SIParameterType.PreselectedSaleID))
            {
                form.ShowDialog();
            }
            await UpdateInvoicesAsync();
        }

        private async void cmsItemOpenInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSaleInvoice = (SaleInvoice)dgvInvoices.SelectedRows[0].DataBoundItem;
            using (var form = new SI_SaleInvoice(selectedSaleInvoice.SaleInvoiceID, SIParameterType.SaleInvoiceID))
            {
                form.ShowDialog();
            }
            await UpdateInvoicesAsync();
        }
        private async void cmsItemDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSaleInvoice = (SaleInvoice)dgvInvoices.SelectedRows[0].DataBoundItem;
            string textMessage = $"Por favor, confirme la eliminación de la siguiente factura:\n\nID Factura: {selectedSaleInvoice.SaleInvoiceID:D8}";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => SaleInvoice.DeleteInvoiceById(selectedSaleInvoice.SaleInvoiceID));
                MessageBox.Show("Factura eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint SA801
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA801 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateInvoicesAsync();
        }

        private void dgvInvoices_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona fila cuando se hace click con el botón derecho.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvInvoices.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvInvoices.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private async Task UpdateInvoicesAsync()
        {
            try
            {
                dgvInvoices.DataSource = await Task.Run(() => SaleInvoice.GetInvoicesBySaleId(SaleID));
            }
            catch (Exception dbException)
            {
                // Waypoint SA802
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA802 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
            }
        }
    }
}
