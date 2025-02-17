using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.TabletApp
{
    public partial class Main : Form
    {
        private enum OrderMode { SmartOrder, OrderByDate };

        private string SearchFilter = string.Empty;
        private OrderMode CurrentOrderMode = OrderMode.SmartOrder;

        public Main()
        {
            InitializeComponent();
            dgvRepairOrders.AutoGenerateColumns = false;
        }
        
        private async void Main_Load(object sender, EventArgs e)
        {
            Logger.SetOutputFilename(Path.Combine(Application.StartupPath, "Clover.TabletApp.log"));
            try
            {
                var settings = Settings.LoadFromFile(Path.Combine(Application.StartupPath, "settings.xml"));
                AppEnvironment.CurrentSettings = settings;
                DbLayerSettings.SetConnectionString(settings.MySqlServerIP, settings.MySqlServerPort, settings.DatabaseName, settings.MySqlUserName, settings.MySqlUserPassword.DecodeBase64());
            }
            catch (Exception exception)
            {
                // Waypoint MA001
                MessageBox.Show("(MA001) Error al cargar la configuración del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA001. Message: " + exception.Message);
                Application.Exit();
                return;
            }
            await UpdateRepairOrdersAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    {
                        btnSignOut.PerformClick();
                        return true;
                    }
                case Keys.Enter:
                    {
                        btnOpenOrder.PerformClick();
                        return true;
                    }
                case (Keys.Control | Keys.N):
                    {
                        btnAddOrder.PerformClick();
                        return true;
                    }
                case Keys.Delete:
                    {
                        btnDeleteOrder.PerformClick();
                        return true;
                    }
                case (Keys.Control | Keys.A):
                    {
                        btnUpdateOrder.PerformClick();
                        return true;
                    }
                case (Keys.Control | Keys.B):
                    {
                        using (var form = new QuickSearch())
                        {
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                SearchFilter = form.SearchInput;
                                lblFilterWarning.Visible = true;
                                var r = UpdateRepairOrdersAsync();
                            }
                        }
                        return true;
                    }
                case (Keys.Control | Keys.R):
                    {
                        SearchFilter = string.Empty;
                        lblFilterWarning.Visible = false;
                        var r = UpdateRepairOrdersAsync();
                        return true;
                    }
                case (Keys.Control | Keys.O):
                    {
                        switch (CurrentOrderMode)
                        {
                            case OrderMode.SmartOrder:
                                {
                                    CurrentOrderMode = OrderMode.OrderByDate;
                                    lblOrderInformation.Text = "Orden por fecha";
                                    break;
                                }
                            case OrderMode.OrderByDate:
                                {
                                    CurrentOrderMode = OrderMode.SmartOrder;
                                    lblOrderInformation.Text = "Orden inteligente";
                                    break;
                                }
                        }
                        var r = UpdateRepairOrdersAsync();
                        return true;
                    }
                case Keys.Up:
                    {
                        if (dgvRepairOrders.Rows.Count > 0)
                        {
                            if (dgvRepairOrders.SelectedRows.Count > 0)
                            {
                                int selectedIndex = dgvRepairOrders.SelectedRows[0].Index;
                                if (selectedIndex > 0)
                                {
                                    dgvRepairOrders.CurrentCell = dgvRepairOrders.Rows[selectedIndex - 1].Cells[0];
                                }
                            }
                            else
                            {
                                dgvRepairOrders.CurrentCell = dgvRepairOrders.Rows[0].Cells[0];
                            }
                        }
                        return true;
                    }
                case Keys.Down:
                    {
                        if (dgvRepairOrders.Rows.Count > 0)
                        {
                            if (dgvRepairOrders.SelectedRows.Count > 0)
                            {
                                int selectedIndex = dgvRepairOrders.SelectedRows[0].Index;
                                if (selectedIndex < (dgvRepairOrders.Rows.Count - 1))
                                {
                                    dgvRepairOrders.CurrentCell = dgvRepairOrders.Rows[selectedIndex + 1].Cells[0];
                                }
                            }
                            else
                            {
                                dgvRepairOrders.CurrentCell = dgvRepairOrders.Rows[0].Cells[0];
                            }
                        }
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void btnOpenOrder_Click(object sender, EventArgs e)
        {
            tmrAutoSingOut.Restart();
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            // Solicita inicio de sesión si es necesario.
            if (AppEnvironment.CurrentUser == null)
            {
                bool auth = RequestSignIn();
                if (auth == false) { return; }
            }
            tmrAutoSingOut.Stop();
            tmrAutoUpdate.Stop();
            // Muestra ventana.
            using (var form = new RO_RepairOrder(selectedRepairOrder.RepairOrderID))
            {
                form.ShowDialog();
            }
            tmrAutoSingOut.Start();
            await UpdateRepairOrdersAsync();
        }
        private async void btnAddOrder_Click(object sender, EventArgs e)
        {
            tmrAutoSingOut.Restart();
            // Solicita inicio de sesión si es necesario.
            if (AppEnvironment.CurrentUser == null)
            {
                bool auth = RequestSignIn();
                if (auth == false) { return; }
            }
            tmrAutoSingOut.Stop();
            tmrAutoUpdate.Stop();
            // Muestra ventana.
            using (var form = new RO_RepairOrder())
            {
                form.ShowDialog();
            }
            tmrAutoSingOut.Start();
            await UpdateRepairOrdersAsync();
        }
        private async void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            tmrAutoSingOut.Restart();
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            // Solicita inicio de sesión si es necesario.
            if (AppEnvironment.CurrentUser == null)
            {
                bool auth = RequestSignIn();
                if (auth == false) { return; }
            }
            // Validaciones.
            if (selectedRepairOrder.EstimateID.HasValue || selectedRepairOrder.Approved || selectedRepairOrder.Completed)
            {
                MessageBox.Show("No es posible realizar la operación: la orden de reparación ya fue cotizada, aprobada o completada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var dialog = MessageBox.Show("Por favor, confirme la operación.", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK)
            {
                return;
            }
            try
            {
                await Task.Run(() => RepairOrder.DeleteRepairOrderById(selectedRepairOrder.RepairOrderID));
                MessageBox.Show("Orden de reparación eliminada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint MA003
                MessageBox.Show("(MA003) Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint MA003 (Flag: MySQL). Message: " + dbException.Message);
            }
            await UpdateRepairOrdersAsync();
        }
        private async void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            tmrAutoSingOut.Restart();
            if (dgvRepairOrders.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedRepairOrder = (RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem;
            // Solicita inicio de sesión si es necesario.
            if (AppEnvironment.CurrentUser == null)
            {
                bool auth = RequestSignIn();
                if (auth == false) { return; }
            }
            tmrAutoSingOut.Stop();
            tmrAutoUpdate.Stop();
            // Muestra ventana.
            using (var form = new RO_OrderUpdate(selectedRepairOrder.RepairOrderID))
            {
                form.ShowDialog();
            }
            tmrAutoSingOut.Start();
            await UpdateRepairOrdersAsync();
        }
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            AppEnvironment.CurrentUser = null;
            lblLoginInformation.Text = string.Empty;
            btnSignOut.Enabled = false;
            MessageBox.Show("Sesión finalizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tmrAutoSingOut.Stop();
        }

        private async void tmrAutoUpdate_Tick(object sender, EventArgs e)
        {
            await UpdateRepairOrdersAsync();
        }
        private void tmrAutoSingOut_Tick(object sender, EventArgs e)
        {
            AppEnvironment.CurrentUser = null;
            lblLoginInformation.Text = string.Empty;
            btnSignOut.Enabled = false;
            MessageBox.Show("Sesión finalizada por inactividad.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tmrAutoSingOut.Stop();
        }

        private void dgvRepairOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var boundItem = (RepairOrder)dgvRepairOrders.Rows[e.RowIndex].DataBoundItem;
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            e.Value = boundItem.RepairOrderID.ToString("D4");
                            switch (boundItem.Status)
                            {
                                case "En curso":
                                    {
                                        dgvRepairOrders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(215, 230, 190);
                                        break;
                                    }
                                case "Rechazado":
                                    {
                                        dgvRepairOrders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(230, 185, 185);
                                        break;
                                    }
                                case "Finalizado":
                                    {
                                        dgvRepairOrders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(185, 205, 230);
                                        break;
                                    }
                            }
                            e.FormattingApplied = true;
                            break;
                        }
                    case 1:
                        {
                            e.Value = boundItem.CustomerName;
                            e.FormattingApplied = true;
                            break;
                        }
                    case 2:
                        {
                            e.Value = boundItem.PumpBrand + " " + boundItem.PumpModel;
                            e.FormattingApplied = true;
                            break;
                        }
                    case 3:
                        {
                            e.Value = boundItem.Status;
                            e.FormattingApplied = true;
                            break;
                        }
                }
            }
        }

        private bool RequestSignIn()
        {
            using (var form = new Login())
            {
                form.ShowDialog();
                if (form.LogInAuthorized)
                {
                    lblLoginInformation.Text = $"Sesión iniciada: {AppEnvironment.CurrentUser.UserName}";
                    btnSignOut.Enabled = true;
                    tmrAutoSingOut.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private async Task UpdateRepairOrdersAsync()
        {
            tmrAutoUpdate.Stop();
            // Construye filtros.
            string havingCommand = string.Empty;
            string orderByCommand = string.Empty;
            if (!string.IsNullOrWhiteSpace(SearchFilter))
            {
                havingCommand = $" HAVING CustomerName LIKE '%{SearchFilter}%'";
            }
            switch (CurrentOrderMode)
            {
                case OrderMode.SmartOrder:
                    {
                        orderByCommand = " ORDER BY repair_order.Completed ASC, repair_order.Approved DESC, priority.Order ASC, repair_order.ApprovalDate DESC, repair_order.Date DESC";
                        break;
                    }
                case OrderMode.OrderByDate:
                    {
                        orderByCommand = " ORDER BY repair_order.Date DESC";
                        break;
                    }
            }
            try
            {
                // Guarda Id del elemento seleccionado.
                int selectedItemId = -1;
                if (dgvRepairOrders.SelectedRows.Count != 0)
                {
                    selectedItemId = ((RepairOrder)dgvRepairOrders.SelectedRows[0].DataBoundItem).RepairOrderID;
                }
                // Recupera información de base de datos.
                dgvRepairOrders.DataSource = await Task.Run(() => RepairOrder.GetRepairOrders(havingCommand + orderByCommand));
                // Restaura Id seleccionado
                var matches = dgvRepairOrders.Rows.Cast<DataGridViewRow>().Where(x => ((RepairOrder)x.DataBoundItem).RepairOrderID == selectedItemId);
                if (matches.Count() != 0)
                {
                    int index = matches.Single().Index;
                    dgvRepairOrders.Rows[index].Selected = true;
                }
                lblConnectionError.Visible = false;
            }
            catch (Exception dbException)
            {
                // Waypoint MA004
                lblConnectionError.Visible = true;
                Logger.AppendLog("Exception at Waypoint MA004 (Flag: MySQL). Message: " + dbException.Message);
                dgvRepairOrders.DataSource = null;
            }
            finally
            {
                tmrAutoUpdate.Start();
            }
        }
    }
}
