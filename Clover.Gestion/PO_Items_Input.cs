using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PO_Items_Input : Form
    {
        private PurchaseOrderItem CurrentItem = null;
        
        public PO_Items_Input(string CurrencySymbol, PurchaseOrderItem Item = null)
        {
            CurrentItem = Item;
            InitializeComponent();
            lblCurrencySymbol_1.Text = CurrencySymbol;
            lblCurrencySymbol_2.Text = CurrencySymbol;
        }

        private async void PO_Items_Input_Load(object sender, EventArgs e)
        {
            List<int> registeredInputs;
            try
            {
                cboVat.DisplayMember = "Description";
                cboVat.ValueMember = "VatID";
                cboVat.DataSource = await Task.Run(() => Vat.GetVats());
                registeredInputs = await Task.Run(() => Input.GetInputsIds());
                if (registeredInputs.Count == 0)
                {
                    MessageBox.Show("No hay insumos registrados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            catch (Exception dbException)
            {
                // Waypoint PO401
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO401 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            var inputsAutoCompleteList = new AutoCompleteStringCollection();
            inputsAutoCompleteList.AddRange(registeredInputs.Select(x => x.ToString("D4")).ToArray());
            txtInput.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtInput.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtInput.AutoCompleteCustomSource = inputsAutoCompleteList;
            if (CurrentItem != null)
            {
                txtInput.Text = CurrentItem.InputID.Value.ToString("D4");
                txtDescription.Text = CurrentItem.Description;
                nudQuantity.Value = CurrentItem.Quantity;
                nudAmount.Value = CurrentItem.Amount;
                cboVat.SelectedValue = CurrentItem.VatID;
            }
            else
            {
                cboVat.SelectedValue = 5; // IVA 21%
            }
        }

        private void btnSearchInput_Click(object sender, EventArgs e)
        {
            using (var form = new PO_Items_Input_InputSearcher())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int selectedId = form.SelectedInputID;
                    txtInput.Text = selectedId.ToString("D4");
                }
            }
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("No hay insumo seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudAmount.Value == 0)
            {
                MessageBox.Show("El precio unitario debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CurrentItem != null)
            {
                CurrentItem.InputID = int.Parse(txtInput.Text);
                CurrentItem.Description = txtDescription.Text;
                CurrentItem.Quantity = nudQuantity.Value;
                CurrentItem.Amount = nudAmount.Value;
                CurrentItem.TotalAmount = (nudQuantity.Value * nudAmount.Value);
                CurrentItem.VatID = (int)cboVat.SelectedValue;
                // Información complementaria requerida para visualización en detalle de ítems.
                CurrentItem.VatPercentage = ((Vat)cboVat.SelectedItem).VatPercentage;
                this.Close();
            }
            else
            {
                ((PO_Items)(this.Owner)).Items.Add(new PurchaseOrderItem()
                {
                    InputID = int.Parse(txtInput.Text),
                    Description = txtDescription.Text,
                    Quantity = nudQuantity.Value,
                    Amount = nudAmount.Value,
                    TotalAmount = (nudQuantity.Value * nudAmount.Value),
                    VatID = (int)cboVat.SelectedValue,
                    // Información complementaria requerida para visualización en detalle de ítems.
                    VatPercentage = ((Vat)cboVat.SelectedItem).VatPercentage
                });
                this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtInput_Validating(object sender, CancelEventArgs e)
        {
            if (txtInput.Text == string.Empty)
            {
                return;
            }
            if (!txtInput.AutoCompleteCustomSource.Contains(txtInput.Text))
            {
                MessageBox.Show("El código ingresado no corresponde a un insumo registrado.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInput.Text = string.Empty;
                txtDescription.Text = string.Empty;
            }
        }
        private async void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.AutoCompleteCustomSource.Contains(txtInput.Text))
            {
                try
                {
                    int selectedInputId = int.Parse(txtInput.Text);
                    var input = await Task.Run(() => Input.GetInputById(selectedInputId));
                    txtDescription.Text = input.Description;
                }
                catch (Exception dbException)
                {
                    // Waypoint PO403
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PO403 (Flag: MySQL). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            txtTotalAmount.Text = (nudQuantity.Value * nudAmount.Value).ToString("N2");
        }
        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            txtTotalAmount.Text = (nudQuantity.Value * nudAmount.Value).ToString("N2");
        }
    }
}
