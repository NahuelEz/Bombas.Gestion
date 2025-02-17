using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class IN_Input : Form
    {
        private int? InputID = null;
        private Input CurrentInput = null;
        private bool SafeExit = false;
        
        public IN_Input(int? InputID = null)
        {
            this.InputID = InputID;
            InitializeComponent();
        }

        private async void IN_InputUI_Load(object sender, EventArgs e)
        {
            if (InputID.HasValue)
            {
                try
                {
                    CurrentInput = await Task.Run(() => Input.GetInputById(InputID.Value));
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryID";
                    cboCategory.DataSource = await Task.Run(() => ItemCategory.GetCategories());
                    cboSubcategory.DisplayMember = "SubcategoryName";
                    cboSubcategory.ValueMember = "SubcategoryID";
                    cboSubcategory.DataSource = await Task.Run(() =>ItemSubcategory.GetSubcategories(CurrentInput.CategoryID));
                }
                catch (Exception dbException)
                {
                    // Waypoint IN201
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint IN201 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                this.Text = "Visualizando insumo: " + InputID.Value.ToString("D8");
                txtInputID.Text = InputID.Value.ToString("D8");
                cboCategory.SelectedValue = CurrentInput.CategoryID;
                cboSubcategory.SelectedValue = CurrentInput.SubcategoryID;
                sbxDescription.Text = CurrentInput.Description;
                cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
            }
            else
            {
                try
                {
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryID";
                    cboCategory.DataSource = await Task.Run(() => ItemCategory.GetCategories());
                    int selectedCategoryID = ((ItemCategory)cboCategory.SelectedItem).CategoryID;
                    cboSubcategory.DisplayMember = "SubcategoryName";
                    cboSubcategory.ValueMember = "SubcategoryID";
                    cboSubcategory.DataSource = await Task.Run(() => ItemSubcategory.GetSubcategories(selectedCategoryID));
                }
                catch (Exception dbException)
                {
                    // Waypoint IN202
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint IN202 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
            }
        }
        private void IN_InputUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentInput == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (string.IsNullOrWhiteSpace(sbxDescription.Text))
            {
                MessageBox.Show("Por favor, complete la descripción del insumo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (sbxDescription.HasSpellingErrors())
            //{
            //    var prompt = MessageBox.Show("La descripción del insumo tiene errores de ortografía."
            //        + Environment.NewLine + Environment.NewLine + "¿Desea continuar de todas formas?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    if (prompt != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}
            // Construye objeto "input".
            var input = new Input()
            {
                InputID = (CurrentInput == null) ? 0 : CurrentInput.InputID,
                CategoryID = (int)cboCategory.SelectedValue,
                SubcategoryID = (int)cboSubcategory.SelectedValue,
                Description = sbxDescription.Text
            };
            if (InputID.HasValue)
            {
                try
                {
                    await Task.Run(() => input.Update());
                    MessageBox.Show("Insumo actualizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint IN203
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint IN203 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => input.Insert());
                    MessageBox.Show("Insumo registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint IN204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint IN204 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedCategoryID = ((ItemCategory)cboCategory.SelectedItem).CategoryID;
                cboSubcategory.DisplayMember = "SubcategoryName";
                cboSubcategory.ValueMember = "SubcategoryID";
                cboSubcategory.DataSource = await Task.Run(() => ItemSubcategory.GetSubcategories(selectedCategoryID));
            }
            catch (Exception dbException)
            {
                // Waypoint IN205
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint IN205 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }
    }
}
