using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_Items : Form
    {
        public BindingList<SaleItem> Items;
        private string CurrentCurrency;
        private bool ReadOnly;

        public SA_Items(List<SaleItem> Items, string CurrentCurrency, bool ReadOnly = false)
        {
            this.Items = new BindingList<SaleItem>(Items);
            this.CurrentCurrency = CurrentCurrency;
            this.ReadOnly = ReadOnly;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = this.Items;
            if (ReadOnly)
            {
                btnAddProduct.Enabled = false;
                btnAddConcept.Enabled = false;
                cmsMoveUp.Enabled = false;
                cmsMoveDown.Enabled = false;
                cmsRemove.Enabled = false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var form = new SA_Items_Product(CurrentCurrency))
            {
                form.ShowDialog(this);
            }
        }
        private void btnAddConcept_Click(object sender, EventArgs e)
        {
            using (var form = new SA_Items_Concept(CurrentCurrency))
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
            var selectedItem = (SaleItem)dgvItems.SelectedRows[0].DataBoundItem;
            if (selectedItem.ProductID.HasValue)
            {
                using (var form = new SA_Items_Product(CurrentCurrency, selectedItem, ReadOnly))
                {
                    form.ShowDialog(this);
                }
            }
            else
            {
                using (var form = new SA_Items_Concept(CurrentCurrency, selectedItem, ReadOnly))
                {
                    form.ShowDialog(this);
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
            // Selecciona fila automáticamente.
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvItems.HitTest(e.X, e.Y);
                if (hitTest.RowIndex != -1)
                {
                    dgvItems.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }
        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Lógica adicional para mostrar imagen del item.
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                var item = (SaleItem)dgvItems.Rows[e.RowIndex].DataBoundItem;
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
    }
}
