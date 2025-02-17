using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PO_Items_Concept : Form
    {
        private PurchaseOrderItem CurrentItem = null;

        public PO_Items_Concept(string CurrencySymbol, PurchaseOrderItem Item = null)
        {
            CurrentItem = Item;
            InitializeComponent();
            lblCurrencySymbol_1.Text = CurrencySymbol;
            lblCurrencySymbol_2.Text = CurrencySymbol;
        }

        private async void PO_Items_Concept_Load(object sender, EventArgs e)
        {
            try
            {
                cboVat.DisplayMember = "Description";
                cboVat.ValueMember = "VatID";
                cboVat.DataSource = await Task.Run(() => Vat.GetVats());
            }
            catch (Exception dbException)
            {
                // Waypoint PO901
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO901 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            if (CurrentItem != null)
            {
                sbxDescription.Text = CurrentItem.Description;
                nudQuantity.Value = CurrentItem.Quantity;
                nudAmount.Value = CurrentItem.Amount;
                cboVat.SelectedValue = CurrentItem.VatID;
            }
            else
            {
                cboVat.SelectedValue = 5; // IVA 21%
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Validación.
            if (string.IsNullOrWhiteSpace(sbxDescription.Text))
            {
                MessageBox.Show("Por favor, complete la descripción del concepto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (sbxDescription.HasSpellingErrors())
            //{
            //    var prompt = MessageBox.Show("La descripción del concepto tiene errores de ortografía."
            //        + Environment.NewLine + Environment.NewLine + "¿Desea continuar de todas formas?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    if (prompt != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            if (nudAmount.Value == 0)
            {
                MessageBox.Show("El precio unitario debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CurrentItem != null)
            {
                CurrentItem.Description = sbxDescription.Text;
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
                    Description = sbxDescription.Text,
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
