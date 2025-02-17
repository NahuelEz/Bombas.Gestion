using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_DeliveryNoteManager : Form
    {
        private int SaleID;

        public SA_DeliveryNoteManager(int SaleID)
        {
            this.SaleID = SaleID;
            InitializeComponent();
            dgvDeliveryNotes.AutoGenerateColumns = false;
        }

        private async void SA_DeliveryNoteManager_Load(object sender, EventArgs e)
        {
            await UpdateDeliveryNotes();
        }

        private async void btnMakeNote_Click(object sender, EventArgs e)
        {
            using (var form = new SA_DeliveryNote(SaleID, DNParameterType.SaleID))
            {
                form.ShowDialog();
            }
            await UpdateDeliveryNotes();
        }
        private async void cmsItemShowDetail_Click(object sender, EventArgs e)
        {
            if (dgvDeliveryNotes.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedDeliveryNote = (DeliveryNote)dgvDeliveryNotes.SelectedRows[0].DataBoundItem;
            using (var form = new SA_DeliveryNote(selectedDeliveryNote.DeliveryNoteID, DNParameterType.DeliveryNoteID))
            {
                form.ShowDialog();
            }
            await UpdateDeliveryNotes();
        }
        private async void cmsItemDeleteDeliveryNote_Click(object sender, EventArgs e)
        {
            if (dgvDeliveryNotes.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedDeliveryNote = (DeliveryNote)dgvDeliveryNotes.SelectedRows[0].DataBoundItem;
            string textMessage = $"Por favor, confirme la eliminación del siguiente remito:\n\nN° de remito: {selectedDeliveryNote.Number}";
            var dialog = MessageBox.Show(textMessage, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => DeliveryNote.DeleteDeliveryNoteById(selectedDeliveryNote.DeliveryNoteID));
                MessageBox.Show("Remito eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint SA301
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA301 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateDeliveryNotes();
        }

        private void dgvDeliveryNotes_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona fila cuando se hace click con el botón derecho.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvDeliveryNotes.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvDeliveryNotes.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private async Task UpdateDeliveryNotes()
        {
            try
            {
                dgvDeliveryNotes.DataSource = await Task.Run(() => DeliveryNote.GetDeliveryNotesBySaleId(SaleID));
            }
            catch (Exception dbException)
            {
                // Waypoint SA302
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA302 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
            }
        }
    }
}
