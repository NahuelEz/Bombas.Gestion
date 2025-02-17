using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class SA_Items_Concept : Form
    {
        private SaleItem CurrentItem = null;
        private byte[] CurrentImage = null;
        
        public SA_Items_Concept(string CurrencySymbol, SaleItem Item = null, bool Readonly = false)
        {
            this.CurrentItem = Item;
            InitializeComponent();
            lblCurrencySymbol_1.Text = CurrencySymbol;
            lblCurrencySymbol_2.Text = CurrencySymbol;
            lblCurrencySymbol_3.Text = CurrencySymbol;
            if (Readonly)
            {
                sbxDescription.Enabled = false;
                nudQuantity.Enabled = false;
                nudAmount.Enabled = false;
                cboVat.Enabled = false;
                btnLoadTemplate.Enabled = false;
                cmsImageOptions.Enabled = false;
            }
        }

        private async void SA_Items_Concept_Load(object sender, EventArgs e)
        {
            try
            {
                cboVat.DisplayMember = "Description";
                cboVat.ValueMember = "VatID";
                cboVat.DataSource = await Task.Run(() => Vat.GetVats());
            }
            catch (Exception dbException)
            {
                // Waypoint SA401
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint SA401 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
                return;
            }
            if (CurrentItem != null)
            {
                sbxDescription.Text = CurrentItem.Description;
                nudQuantity.Value = CurrentItem.Quantity;
                if (CurrentItem.Cost.HasValue)
                {
                    nudCost.Value = CurrentItem.Cost.Value;
                    nudCost.Enabled = true;
                    chkCostNotSpecified.Checked = false;
                }
                nudAmount.Value = CurrentItem.Amount;
                txtTotalAmount.Text = CurrentItem.TotalAmount.ToString("N2");
                cboVat.SelectedValue = CurrentItem.VatID;
                if (CurrentItem.DeliveryDelay.HasValue)
                {
                    nudDeliveryDelay.Value = CurrentItem.DeliveryDelay.Value;
                    nudDeliveryDelay.Enabled = true;
                    label9.Enabled = true;
                    chkDeliveryNotSpecified.Checked = false;
                }
                if (CurrentItem.CustomImage != null)
                {
                    pbxImagePreview.Image = CurrentItem.CustomImage.ToImage();
                    CurrentImage = CurrentItem.CustomImage;
                }
            }
            else
            {
                cboVat.SelectedValue = 5; // IVA 21%
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(sbxDescription.Text))
            {
                MessageBox.Show("Por favor, complete la descripción del concepto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!chkCostNotSpecified.Checked && nudCost.Value == 0)
            {
                MessageBox.Show("El costo unitario debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudAmount.Value == 0)
            {
                MessageBox.Show("El precio unitario debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (CurrentItem == null)
            {
                ((SA_Items)(this.Owner)).Items.Add(new SaleItem()
                {
                    Description = sbxDescription.Text,
                    DeliveryDelay = chkDeliveryNotSpecified.Checked ? new int?() : (int)(nudDeliveryDelay.Value),
                    Quantity = nudQuantity.Value,
                    Cost = chkCostNotSpecified.Checked ? new decimal?() : nudCost.Value,
                    Amount = nudAmount.Value,
                    TotalAmount = (nudQuantity.Value * nudAmount.Value),
                    VatID = ((Vat)cboVat.SelectedItem).VatID,
                    CustomImage = CurrentImage,
                    // Información complementaria requerida para visualización en detalle de ítems.
                    VatPercentage = ((Vat)cboVat.SelectedItem).VatPercentage
                });
                this.Close();
            }
            else
            {
                CurrentItem.Description = sbxDescription.Text;
                CurrentItem.DeliveryDelay = chkDeliveryNotSpecified.Checked ? new int?() : (int)(nudDeliveryDelay.Value);
                CurrentItem.Quantity = nudQuantity.Value;
                CurrentItem.Cost = chkCostNotSpecified.Checked ? new decimal?() : nudCost.Value;
                CurrentItem.Amount = nudAmount.Value;
                CurrentItem.TotalAmount = (nudQuantity.Value * nudAmount.Value);
                CurrentItem.VatID = ((Vat)cboVat.SelectedItem).VatID;
                CurrentItem.CustomImage = CurrentImage;
                // Información complementaria requerida para visualización en detalle de ítems.
                CurrentItem.VatPercentage = ((Vat)cboVat.SelectedItem).VatPercentage;
                this.Close();
            }
        }
        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            sbxDescription.Text = "Reparación bomba { }"
                                + Environment.NewLine + "- Desarme del equipo"
                                + Environment.NewLine + "- Bobinado de motor de { }"
                                + Environment.NewLine + "- Cambio de rodamientos { }"
                                + Environment.NewLine + "- Retenes { }"
                                + Environment.NewLine + "- Sellos mecánicos { }"
                                + Environment.NewLine + "- Pulido del eje"
                                + Environment.NewLine + "- Capacitor nuevo { } uF"
                                + Environment.NewLine + "- Kit de O'rings"
                                + Environment.NewLine + "- Tornillos nuevos"
                                + Environment.NewLine + "- Limpieza"
                                + Environment.NewLine + "- Armado"
                                + Environment.NewLine + "- Pintura"
                                + Environment.NewLine + "- Pruebas hidráulicas y dinámicas";
        }

        private void cmsOpenImage_Click(object sender, EventArgs e)
        {
            if (ofdOpenImage.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (pbxImagePreview.Image != null)
            {
                pbxImagePreview.Image.Dispose();
                pbxImagePreview.Image = null;
            }
            CurrentImage = null;
            try
            {
                var image = System.Drawing.Image.FromFile(ofdOpenImage.FileName);
                (image as System.Drawing.Bitmap).SetResolution(96, 96);
                pbxImagePreview.Image = image.Resize(125, 125);
                CurrentImage = pbxImagePreview.Image.ToByteArray();
            }
            catch (Exception exception)
            {
                // Waypoint SA402
                MessageBox.Show("Error cargar la imagen del concepto."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.AppendLog("Exception at Waypoint SA402. Message: " + exception.Message);
            }
        }
        private void cmsClearImage_Click(object sender, EventArgs e)
        {
            if (pbxImagePreview.Image != null)
            {
                pbxImagePreview.Image.Dispose();
                pbxImagePreview.Image = Properties.Resources.EmptyImageIcon;
            }
            CurrentImage = null;
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            txtTotalAmount.Text = (nudQuantity.Value * nudAmount.Value).ToString("N2");
        }
        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            txtTotalAmount.Text = (nudQuantity.Value * nudAmount.Value).ToString("N2");
        }
        private void chkDeliveryNotSpecified_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeliveryNotSpecified.Checked)
            {
                nudDeliveryDelay.Enabled = false;
                label9.Enabled = false;
            }
            else
            {
                nudDeliveryDelay.Enabled = true;
                label9.Enabled = true;
            }
        }
        private void chkCostNotSpecified_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCostNotSpecified.Checked)
            {
                nudCost.Enabled = false;
            }
            else
            {
                nudCost.Enabled = true;
            }
        }
    }
}
