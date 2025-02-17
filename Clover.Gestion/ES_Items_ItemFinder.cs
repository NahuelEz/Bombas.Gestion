using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ES_Items_ItemFinder : Form
    {
        private int? CustomerID;

        public ES_Items_ItemFinder(int? CustomerID = null)
        {
            this.CustomerID = CustomerID;

            InitializeComponent();

            // Evita que se generen columnas automáticamente al establecer el origen de datos de la tabla.
            dgvItems.AutoGenerateColumns = false;
        }

        private async void ES_Items_ItemFinder_Load(object sender, EventArgs e)
        {
            // Carga las listas de clientes y presupuestos.
            try
            {
                cboCustomer.DisplayMember = "CustomerName";
                cboCustomer.ValueMember = "CustomerID";
                cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                cboEstimate.ValueMember = "EstimateID";
                cboEstimate.DataSource = await Task.Run(() => Estimate.GetEstimatesLight());
            }
            catch (Exception dbException)
            {
                // Waypoint ES701
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES701 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }

            // Valor predeterminado para el selector de cliente
            if (CustomerID.HasValue)
            {
                cboCustomer.SelectedValue = CustomerID.Value;
            }

            // Valores predeterminados de los selectores de fecha (1 mes hacia atras).
            dtpDateFrom.Value = DateTime.Today.AddMonths(-1);
            dtpDateTo.Value = DateTime.Today;
        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            // Avisa al usuario si hay ítems seleccionados.
            var atLeastOneSelected = dgvItems.Rows
                .Cast<DataGridViewRow>()
                .Any(X => Convert.ToBoolean(((DataGridViewCheckBoxCell)X.Cells["selectionColumn"]).EditedFormattedValue));
            if (atLeastOneSelected)
            {
                var dialog = MessageBox.Show("Al realizar una nueva búsqueda, se perderán los ítems seleccionados. ¿Desea continuar?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog != DialogResult.OK)
                {
                    return;
                }
            }

            if (rbnFilterByDate.Checked)
            {
                // Verifica que haya un cliente seleccionado.
                if (cboCustomer.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verifica que las fechas sean coherentes.
                if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
                {
                    MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de cierre.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Carga los ítems de presupuestos del cliente y fecha seleccionados.
                int customerId = (int)cboCustomer.SelectedValue;
                var dateFrom = dtpDateFrom.Value.Date;
                var dateTo = dtpDateTo.Value.Date;
                try
                {
                    dgvItems.DataSource = await Task.Run(() => EstimateItem.GetItemsByCustomerId(customerId, dateFrom, dateTo, 50));
                }
                catch (Exception dbException)
                {
                    // Waypoint ES702
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES702 (Flag: MySQL). Message: " + dbException.Message);
                    dgvItems.DataSource = null;
                    return;
                }
            }
            else
            {
                // Verifica que haya un presupuesto seleccionado.
                if (cboEstimate.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un presupuesto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Carga los ítems del presupuesto seleccionado.
                int estimateId = (int)cboEstimate.SelectedValue;
                try
                {
                    dgvItems.DataSource = await Task.Run(() => EstimateItem.GetItemsByEstimateId(estimateId));
                }
                catch (Exception dbException)
                {
                    // Waypoint ES703
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES703 (Flag: MySQL). Message: " + dbException.Message);
                    dgvItems.DataSource = null;
                    return;
                }
            }

            // Avisa al usuario si no se encontraron ítems.
            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron ítems con el criterio especificado.", "Atencíon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            var selectedItems = dgvItems.Rows
                .Cast<DataGridViewRow>()
                .Where(X => Convert.ToBoolean(((DataGridViewCheckBoxCell)X.Cells["selectionColumn"]).EditedFormattedValue))
                .Select(X => (EstimateItem)X.DataBoundItem)
                .ToList();

            // Verifica que haya al menos un item seleccionado.
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("No hay ítems seleccionados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agrega los ítems al presupuesto actual.
            foreach (var item in selectedItems)
            {
                ((ES_Items)(this.Owner)).Items.Add(item.CopyItem());
            }

            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbnFilterByDate_CheckedChanged(object sender, EventArgs e)
        {
            // Activa y desactiva los controles de filtro por fecha.
            cboCustomer.Enabled = rbnFilterByDate.Checked;
            label1.Enabled = rbnFilterByDate.Checked;
            label2.Enabled = rbnFilterByDate.Checked;
            dtpDateFrom.Enabled = rbnFilterByDate.Checked;
            dtpDateTo.Enabled = rbnFilterByDate.Checked;
        }
        private void rbnFilterByEstimate_CheckedChanged(object sender, EventArgs e)
        {
            // Activa y desactiva los controles de filtro por presupuesto.
            cboEstimate.Enabled = rbnFilterByEstimate.Checked;
        }
        private void cboEstimate_Format(object sender, ListControlConvertEventArgs e)
        {
            // Formatea el valor mostrado en la lista de presupuestos.
            var estimate = (Estimate)e.ListItem;
            e.Value = $"{estimate.EstimateID} - {estimate.CustomerName}";
        }
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            // Evita que se seleccione cualquier fila.
            dgvItems.ClearSelection();
        }
        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Lógica adicional para mostrar imagen del item.
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                var item = (EstimateItem)dgvItems.Rows[e.RowIndex].DataBoundItem;
                if (item.CustomImage != null)
                {
                    e.Value = item.CustomImage.ToImage();
                }
                else if (item.ProductImage != null)
                {
                    e.Value = item.ProductImage.ToImage();
                }
                else
                {
                    e.Value = null;
                }
            }
        }
        private void cboCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Borra el texto ingresado si no es un elemento de la lista.
            if (cboCustomer.SelectedItem == null)
            {
                cboCustomer.Text = string.Empty;
            }
        }
        private void cboEstimate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cboEstimate.SelectedItem == null)
            {
                cboEstimate.Text = string.Empty;
            }
        }
    }
}
