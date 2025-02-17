using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PO_Items : Form
    {
        public BindingList<PurchaseOrderItem> Items;
        private string CurrentCurrency;

        public PO_Items(List<PurchaseOrderItem> Items, string CurrentCurrency)
        {
            this.Items = new BindingList<PurchaseOrderItem>(Items);
            this.CurrentCurrency = CurrentCurrency;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = this.Items;
        }

        private void btnAddInput_Click(object sender, EventArgs e)
        {
            using (var form = new PO_Items_Input(CurrentCurrency))
            {
                form.ShowDialog(this);
            }
        }
        private void btnAddConcept_Click(object sender, EventArgs e)
        {
            using (var form = new PO_Items_Concept(CurrentCurrency))
            {
                form.ShowDialog(this);
            }
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedItem = (PurchaseOrderItem)dgvItems.SelectedRows[0].DataBoundItem;
            if (selectedItem.InputID.HasValue)
            {
                using (var form = new PO_Items_Input(CurrentCurrency, selectedItem))
                {
                    form.ShowDialog(this);
                }
            }
            else
            {
                using (var form = new PO_Items_Concept(CurrentCurrency, selectedItem))
                {
                    form.ShowDialog(this);
                }
            }
        }
        private void cmsCopy_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                return;
            }
            var selectedItem = (PurchaseOrderItem)dgvItems.SelectedRows[0].DataBoundItem;
            Clipboard.Clear();
            var dataObject = new DataObject();
            dataObject.SetData("Clover.Gestion.PurchaseOrderItem", selectedItem.CopyItem());
            dataObject.SetData(DataFormats.UnicodeText, string.Concat(
                      selectedItem.Quantity.ToStringPreferIntegerFormat(),
                "\t", selectedItem.Description.Replace(Environment.NewLine, " "),
                "\t", selectedItem.VatPercentage.ToStringPreferIntegerFormat(),
                "\t", selectedItem.Amount.ToString("N2"),
                "\t", selectedItem.TotalAmount.ToString("N2")));
            Clipboard.SetDataObject(dataObject);
        }
        private void cmsPaste_Click(object sender, EventArgs e)
        {
            var dataObject = Clipboard.GetDataObject();
            if (dataObject == null)
            {
                return;
            }
            if (dataObject.GetDataPresent("Clover.Gestion.PurchaseOrderItem"))
            {
                var item = (PurchaseOrderItem)dataObject.GetData("Clover.Gestion.PurchaseOrderItem");
                if (item != null)
                {
                    Items.Add(item.CopyItem());
                }
            }
        }
        private void cmsMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dgvItems.SelectedRows[0].Index;
            if (index == 0)
            {
                return;
            }
            var item = Items[index];
            Items.RemoveAt(index);
            Items.Insert(index - 1, item);
            dgvItems.Rows[index - 1].Selected = true;
        }
        private void cmsMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                return;
            }
            int index = dgvItems.SelectedRows[0].Index;
            if (index == (Items.Count - 1))
            {
                return;
            }
            var item = Items[index];
            Items.RemoveAt(index);
            Items.Insert(index + 1, item);
            dgvItems.Rows[index + 1].Selected = true;
        }
        private void cmsRemove_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                return;
            }
            var promptMessage = MessageBox.Show("Por favor, confirme la eliminación del ítem.", "Atencíon", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (promptMessage == DialogResult.OK)
            {
                Items.RemoveAt(dgvItems.SelectedRows[0].Index);
            }
        }

        private void dgvItems_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecciona automáticamente la fila cuando se hace click con el botón derecho.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvItems.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvItems.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }
    }
}
