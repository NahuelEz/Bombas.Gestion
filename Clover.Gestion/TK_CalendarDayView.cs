using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class TK_CalendarDayView : Form
    {
        private DateTime Date;

        public TK_CalendarDayView(DateTime Date)
        {
            this.Date = Date;
            InitializeComponent();
            dgvTasks.AutoGenerateColumns = false;
            dgvSales.AutoGenerateColumns = false;
        }

        private async void TK_CalendarDayView_Load(object sender, EventArgs e)
        {
            this.Text = "Resumen diario: " + Date.ToString("dd/MM/yyyy");
            lblDate.Text = Date.ToString("dddd, d 'de' MMMM 'de' yyyy");
            await UpdateTasksAsync();
            await UpdateShipmentsAsync();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void btnExportTasks_Click(object sender, EventArgs e)
        {
            var pendingTasks = ((List<ScheduledTask>)dgvTasks.DataSource).Where(X => !X.Completed).OrderBy(X => X.Priority).ToList();
            // Validaciones
            if (pendingTasks.Count == 0)
            {
                MessageBox.Show("No se registran tareas pendientes.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Selección del usuario.
            List<ScheduledTask> selectedTasks = null;
            using (var form = new TK_TaskSelector(pendingTasks))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedTasks = form.SelectedTasks;
                }
                else
                {
                    return;
                }
            }
            // Exporta documento PDF.
            string documentTitle = Date.ToString("dddd, d 'de' MMMM 'de' yyyy");
            float[] columnWidths = { 5, 1 };
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn() { ColumnName = "column1", Caption = "Tarea", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "column2", Caption = "Prioridad", DataType = typeof(string) });
            foreach (var task in selectedTasks)
            {
                dataTable.Rows.Add(task.Description, task.Priority);
            }
            string pdfPath = Path.Combine(Path.GetTempPath(), $"pendientes_{DateTime.Now:ddMMyyHHmmss}.pdf");
            try
            {
                // Generación documento PDF.
                await Task.Run(() => PdfGeneration.ExportPdfDataTable(documentTitle, columnWidths, dataTable, pdfPath));
            }
            catch (Exception pdfExportException)
            {
                // Waypoint TK201
                MessageBox.Show("Error al exportar documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfExportException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK201. Message: " + pdfExportException.Message);
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
                // Waypoint TK202
                MessageBox.Show("Error al abrir documento PDF."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + pdfOpenException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK202. Message: " + pdfOpenException.Message);
                return;
            }
        }

        private void dgvDelta_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona fila cuando se hace click con el botón derecho.
            var target = (DataGridView)sender;
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = target.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    target.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private async void cmsItemOpenTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedTask = (ScheduledTask)dgvTasks.SelectedRows[0].DataBoundItem;
            using (var form = new TK_Task(selectedTask.TaskID))
            {
                form.ShowDialog();
            }
            await UpdateTasksAsync();
        }
        private async void cmsItemSetTaskAsCompleted_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedTask = (ScheduledTask)dgvTasks.SelectedRows[0].DataBoundItem;
            if (selectedTask.Completed)
            {
                MessageBox.Show("La condición actual de la tarea es COMPLETADA.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                await Task.Run(() => ScheduledTask.SetTaskCompleted(selectedTask.TaskID, true));
            }
            catch (Exception dbException)
            {
                // Waypoint TK203
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK203 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateTasksAsync();
        }
        private async void cmsItemSetTaskAsNonCompleted_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedTask = (ScheduledTask)dgvTasks.SelectedRows[0].DataBoundItem;
            if (!selectedTask.Completed)
            {
                MessageBox.Show("La condición actual de la tarea es NO COMPLETADA.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                await Task.Run(() => ScheduledTask.SetTaskCompleted(selectedTask.TaskID, false));
            }
            catch (Exception dbException)
            {
                // Waypoint TK204
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK204 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateTasksAsync();
        }
        private async void cmsItemDeleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedTask = (ScheduledTask)dgvTasks.SelectedRows[0].DataBoundItem;
            var dialog = MessageBox.Show("Por favor, confirme la eliminación de la tarea.", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => ScheduledTask.DeleteTaskById(selectedTask.TaskID));
            }
            catch (Exception dbException)
            {
                // Waypoint TK205
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK205 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateTasksAsync();
        }

        private async void cmsItemSetShipmentAsShipped_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (selectedSale.Shipped)
            {
                MessageBox.Show("La condición actual del envío es DESPACHADO.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var form = new SA_RecordShipment(selectedSale.SaleID))
            {
                form.ShowDialog();
            }
            await UpdateShipmentsAsync();
        }
        private async void cmsItemSetShipmentAsPending_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            if (!selectedSale.Shipped)
            {
                MessageBox.Show("La condición actual del envío es PENDIENTE.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                await Task.Run(() => Sale.UpdateShippingInformation(selectedSale.SaleID, false, null, null));
            }
            catch (Exception dbException)
            {
                // Waypoint TK206
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK206 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateShipmentsAsync();
        }
        private async void cmsItemGoToSaleFromShipment_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            using (var form = new SA_Sale(selectedSale.SaleID, SAParameterType.SaleID))
            {
                form.ShowDialog();
            }
            await UpdateShipmentsAsync();
        }
        private void cmsItemGoToCustomerFromShipment_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            using (var form = new CU_Customer(selectedSale.CustomerID))
            {
                form.ShowDialog();
            }
        }

        private async Task UpdateTasksAsync()
        {
            try
            {
                dgvTasks.DataSource = await Task.Run(() => ScheduledTask.GetTasksByDate(Date));
                // Pinta de verde las tareas ya completadas.
                foreach (DataGridViewRow row in dgvTasks.Rows)
                {
                    if (((ScheduledTask)row.DataBoundItem).Completed)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(184, 204, 228);
                    }
                }
            }
            catch (Exception dbException)
            {
                // Waypoint TK207
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK207 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
            }
        }
        private async Task UpdateShipmentsAsync()
        {
            try
            {
                dgvSales.DataSource = await Task.Run(() => Sale.GetSalesByDeliveryDate(Date));
                // Pinta de verde las ventas ya despachadas.
                foreach (DataGridViewRow row in dgvSales.Rows)
                {
                    if (((Sale)row.DataBoundItem).Shipped)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(184, 204, 228);
                    }
                }
            }
            catch (Exception dbException)
            {
                // Waypoint TK208
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK208 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
            }
        }

        private void dgvSales_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Lógica adicional para mostrar el estado de la venta como texto.
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                if (((Sale)dgvSales.Rows[e.RowIndex].DataBoundItem).Shipped)
                {
                    e.Value = "Despachado";
                }
                else
                {
                    e.Value = "Pendiente";
                }
            }
        }
    }
}
