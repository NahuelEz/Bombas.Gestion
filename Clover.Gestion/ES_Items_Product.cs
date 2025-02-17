using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ES_Items_Product : Form
    {
        private EstimateItem CurrentItem = null;
        private Product AssociatedProduct = null;
        
        public ES_Items_Product(string CurrencySymbol, EstimateItem Item = null)
        {
            this.CurrentItem = Item;
            InitializeComponent();
            lblCurrencySymbol_1.Text = CurrencySymbol;
            lblCurrencySymbol_2.Text = CurrencySymbol;
        }

        private async void ES_Items_Product_Load(object sender, EventArgs e)
        {
            List<string> partCodes;
            try
            {
                partCodes = await Task.Run(() => Product.GetPartCodes());
                cboVat.DisplayMember = "Description";
                cboVat.ValueMember = "VatID";
                cboVat.DataSource = await Task.Run(() => Vat.GetVats());
            }
            catch (Exception dbException)
            {
                // Waypoint ES501
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ES501 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
                return;
            }
            var productsAutoCompleteList = new AutoCompleteStringCollection();
            productsAutoCompleteList.AddRange(partCodes.ToArray());
            txtProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtProduct.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtProduct.AutoCompleteCustomSource = productsAutoCompleteList;
            if (CurrentItem != null)
            {
                try
                {
                    AssociatedProduct = await Task.Run(() => Product.GetProductById(CurrentItem.ProductID.Value));
                }
                catch (Exception dbException)
                {
                    // Waypoint ES502
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES502 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                sbxDescription.Text = CurrentItem.Description;
                txtProduct.Text = AssociatedProduct.PartCode;
                nudQuantity.Value = CurrentItem.Quantity;
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
                if (AssociatedProduct.ProductImage != null)
                {
                    try
                    {
                        pbxImagePreview.Image = AssociatedProduct.ProductImage.ToImage();
                    }
                    catch (Exception exception)
                    {
                        // Waypoint ES503
                        MessageBox.Show("Error cargar la imagen del producto."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Logger.AppendLog("Exception at Waypoint ES503. Message: " + exception.Message);
                    }
                }
            }
            else
            {
                cboVat.SelectedValue = 5; // IVA 21%
            }
            txtProduct.Validating += txtProduct_Validating;
            txtProduct.TextChanged += txtProduct_TextChanged;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Validación
            if (AssociatedProduct == null)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(sbxDescription.Text))
            {
                MessageBox.Show("Por favor, complete la descripción del producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (sbxDescription.HasSpellingErrors())
            //{
            //    var prompt = MessageBox.Show("La descripción del producto tiene errores de ortografía."
            //        + Environment.NewLine + Environment.NewLine + "¿Desea continuar de todas formas?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    if (prompt != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            if (CurrentItem == null)
            {
                ((ES_Items)(this.Owner)).Items.Add(new EstimateItem()
                {
                    Description = sbxDescription.Text,
                    DeliveryDelay = chkDeliveryNotSpecified.Checked ? new int?() : (int)(nudDeliveryDelay.Value),
                    ProductID = AssociatedProduct.ProductID,
                    Quantity = nudQuantity.Value,
                    Amount = nudAmount.Value,
                    TotalAmount = (nudQuantity.Value * nudAmount.Value),
                    VatID = (int)cboVat.SelectedValue,
                    ProductImage = AssociatedProduct.ProductImage,
                    // Información complementaria requerida para visualización en detalle de ítems.
                    VatPercentage = ((Vat)cboVat.SelectedItem).VatPercentage,
                });
                this.Close();
            }
            else
            {
                CurrentItem.Description = sbxDescription.Text;
                CurrentItem.DeliveryDelay = chkDeliveryNotSpecified.Checked ? new int?() : (int)(nudDeliveryDelay.Value);
                CurrentItem.ProductID = AssociatedProduct.ProductID;
                CurrentItem.Quantity = nudQuantity.Value;
                CurrentItem.Amount = nudAmount.Value;
                CurrentItem.TotalAmount = (nudQuantity.Value * nudAmount.Value);
                CurrentItem.VatID = (int)cboVat.SelectedValue;
                CurrentItem.ProductImage = AssociatedProduct.ProductImage;
                // Información complementaria requerida para visualización en detalle de ítems.
                CurrentItem.VatPercentage = ((Vat)cboVat.SelectedItem).VatPercentage;
                this.Close();
            }
        }
        private void btnAutoFormat_Click(object sender, EventArgs e)
        {
            if (AssociatedProduct == null)
            {
                MessageBox.Show("No hay producto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!AssociatedProduct.IsSeal)
            {
                MessageBox.Show("La opción de formato automático solo está disponible para sellos mecánicos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var form = new ES_Items_Product_AutoFormat(AssociatedProduct.PartCode, AssociatedProduct.TypeDescription))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sbxDescription.Text = form.FormattedDescription;
                }
            }
        }

        private void txtProduct_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!txtProduct.AutoCompleteCustomSource.Contains(txtProduct.Text))
            {
                if (txtProduct.Text != string.Empty)
                {
                    MessageBox.Show("El código ingresado no corresponde a un producto registrado.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                sbxDescription.Text = string.Empty;
                txtProduct.Text = string.Empty;
                if (pbxImagePreview.Image != null)
                {
                    pbxImagePreview.Image.Dispose();
                    pbxImagePreview.Image = Properties.Resources.EmptyImageIcon;
                }
                AssociatedProduct = null;
            }
        }
        private async void txtProduct_TextChanged(object sender, EventArgs e)
        {
            if (txtProduct.AutoCompleteCustomSource.Contains(txtProduct.Text))
            {
                try
                {
                    string selectedProductPartCode = txtProduct.Text;
                    AssociatedProduct = await Task.Run(() => Product.GetProductByPartCode(selectedProductPartCode));
                }
                catch (Exception dbException)
                {
                    // Waypoint ES504
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ES504 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                if (AssociatedProduct.IsSeal)
                {
                    sbxDescription.Text = "Sello mecánico tipo { }. Modelo: { }, para eje de { }."
                                        + Environment.NewLine + "Materiales: la pista estacionaria es de { }, la pista rotativa es de { }, "
                                        + "los elastómeros son de { } y las demás partes de acero inoxidable.";
                }
                else
                {
                    sbxDescription.Text = AssociatedProduct.Description;
                }
                if (AssociatedProduct.ProductImage != null)
                {
                    if (pbxImagePreview.Image != null)
                    {
                        pbxImagePreview.Image.Dispose();
                    }
                    try
                    {
                        pbxImagePreview.Image = AssociatedProduct.ProductImage.ToImage();
                    }
                    catch (Exception exception)
                    {
                        // Waypoint ES505
                        MessageBox.Show("Error cargar la imagen del producto."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Logger.AppendLog("Exception at Waypoint ES505. Message: " + exception.Message);
                    }
                }
                else
                {
                    if (pbxImagePreview.Image != null)
                    {
                        pbxImagePreview.Image.Dispose();
                        pbxImagePreview.Image = Properties.Resources.EmptyImageIcon;
                    }
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
    }
}
