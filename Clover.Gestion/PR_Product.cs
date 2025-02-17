using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PR_Product : Form
    {
        private int? ProductID = null;
        private Product CurrentProduct = null;
        private byte[] CurrentImage = null;
        private bool SafeExit = false;
        
        public PR_Product(int? ProductID = null)
        {
            this.ProductID = ProductID;
            InitializeComponent();
        }

        private async void PR_Product_Load(object sender, EventArgs e)
        {
            if (ProductID.HasValue)
            {
                try
                {
                    CurrentProduct = await Task.Run(() => Product.GetProductById(ProductID.Value));
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                    cboTypeOfSeal.DisplayMember = "TypeDescription";
                    cboTypeOfSeal.ValueMember = "SealTypeID";
                    cboTypeOfSeal.DataSource = await Task.Run(() => SealType.GetSealTypes());
                }
                catch (Exception dbException)
                {
                    // Waypoint PR201
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PR201 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
                this.Text = $"Visualizando producto: {CurrentProduct.PartCode}";
                txtPartCode.Text = CurrentProduct.PartCode;
                if (CurrentProduct.IsSeal)
                {
                    rbnIsSeal.Checked = true;
                    cboTypeOfSeal.SelectedValue = CurrentProduct.SealTypeID.Value;
                }
                else
                {
                    rbnIsOther.Checked = true;
                    sbxDescription.Text = CurrentProduct.Description;
                }
                nudUnitPrice.Value = CurrentProduct.UnitPrice;
                cboCurrency.SelectedValue = CurrentProduct.CurrencyID;
                txtInitialStock.Text = CurrentProduct.Stock.ToString("N2");
                txtStock.Text = CurrentProduct.Stock.ToString("N2");
                if (CurrentProduct.ProductImage != null)
                {
                    try
                    {
                        pbxImagePreview.Image = CurrentProduct.ProductImage.ToImage();
                        CurrentImage = CurrentProduct.ProductImage;
                    }
                    catch (Exception knownException)
                    {
                        // Waypoint PR202
                        MessageBox.Show("Error al leer la imagen del producto."
                            + Environment.NewLine + Environment.NewLine + "Mensaje: " + knownException.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Logger.AppendLog("Exception at Waypoint PR202. Message: " + knownException.Message);
                    }
                }
                btnAccept.Text = "Guardar cambios";
            }
            else
            {
                try
                {
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                    cboTypeOfSeal.DisplayMember = "TypeDescription";
                    cboTypeOfSeal.ValueMember = "SealTypeID";
                    cboTypeOfSeal.DataSource = await Task.Run(() => SealType.GetSealTypes());
                }
                catch (Exception dbException)
                {
                    // Waypoint PR203
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PR203 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                    return;
                }
            }
        }
        private void PR_Product_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentProduct == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtPartCode.Text))
            {
                MessageBox.Show("Por favor, complete el código de parte del producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Construye objeto "product"
            var product = new Product()
            {
                ProductID = (CurrentProduct == null) ? 0 : CurrentProduct.ProductID,
                PartCode = txtPartCode.Text,
                IsSeal = rbnIsSeal.Checked,
                SealTypeID = (rbnIsSeal.Checked) ? (int)cboTypeOfSeal.SelectedValue : new int?(),
                Description = sbxDescription.Text.NullIfEmpty(),
                Stock = (CurrentProduct == null) ? nudChangeStock.Value : CurrentProduct.Stock + nudChangeStock.Value,
                UnitPrice = nudUnitPrice.Value,
                CurrencyID = (int)cboCurrency.SelectedValue,
                ProductImage = CurrentImage
            };
            if (CurrentProduct == null)
            {
                try
                {
                    await Task.Run(() => product.Insert());
                    MessageBox.Show("Producto registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint PR204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PR204 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => product.Update());
                    MessageBox.Show("Producto actualizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint PR205
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PR205 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            ofdOpenImage.FileName = txtPartCode.Text + ".png";
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
            catch (Exception knownException)
            {
                // Waypoint PR206
                MessageBox.Show("Error cargar la imagen del producto."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + knownException.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.AppendLog("Exception at Waypoint PR206. Message: " + knownException.Message);
            }
        }
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            if (pbxImagePreview.Image != null)
            {
                pbxImagePreview.Image.Dispose();
                pbxImagePreview.Image = Properties.Resources.EmptyImageIcon;
            }
            CurrentImage = null;
        }
        private void nudChangeStock_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentProduct == null)
            {
                txtStock.Text = nudChangeStock.Value.ToString("N2");
            }
            else
            {
                txtStock.Text = (CurrentProduct.Stock + nudChangeStock.Value).ToString("N2");
            }
        }
        private void rbnIsSeal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnIsSeal.Checked)
            {
                lbl4.Enabled = false;
                sbxDescription.Enabled = false;
                sbxDescription.Text = string.Empty;
                lbl3.Enabled = true;
                cboTypeOfSeal.Enabled = true;
            }
        }
        private void rbnIsOther_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnIsOther.Checked)
            {
                lbl3.Enabled = false;
                cboTypeOfSeal.Enabled = false;
                lbl4.Enabled = true;
                sbxDescription.Enabled = true;
            }
        }
    }
}
