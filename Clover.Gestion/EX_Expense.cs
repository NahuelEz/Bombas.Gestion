using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class EX_Expense : Form
    {
        private int? ExpenseID = null;
        private Expense CurrentExpense = null;
        private bool SafeExit = false;

        public EX_Expense(int? ExpenseID = null)
        {
            this.ExpenseID = ExpenseID;
            InitializeComponent();
        }

        private async void EX_Expense_Load(object sender, EventArgs e)
        {
            if (ExpenseID.HasValue)
            {
                try
                {
                    CurrentExpense = await Task.Run(() => Expense.GetExpenseById(ExpenseID.Value));
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryID";
                    cboCategory.DataSource = await Task.Run(() => ItemCategory.GetCategories());
                    cboSubcategory.DisplayMember = "SubcategoryName";
                    cboSubcategory.ValueMember = "SubcategoryID";
                    cboSubcategory.DataSource = await Task.Run(() => ItemSubcategory.GetSubcategories(CurrentExpense.CategoryID));
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint EX201
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint EX201 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                this.Text = $"Visualizando gasto: {CurrentExpense.ExpenseID:D8}";
                txtExpenseID.Text = CurrentExpense.ExpenseID.ToString("D8");
                dtpExpenseDate.Value = CurrentExpense.Date;
                cboCategory.SelectedValue = CurrentExpense.CategoryID;
                cboSubcategory.SelectedValue = CurrentExpense.SubcategoryID;
                txtDescription.Text = CurrentExpense.Description;
                nudAmount.Value = CurrentExpense.Amount;
                cboCurrency.SelectedValue = CurrentExpense.CurrencyID;
                txtInvoiceNumber.Text = CurrentExpense.InvoiceNumber;
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
                    cboCurrency.DisplayMember = "CurrencyName";
                    cboCurrency.ValueMember = "CurrencyID";
                    cboCurrency.DataSource = await Task.Run(() => Currency.GetCurrencies());
                }
                catch (Exception dbException)
                {
                    // Waypoint EX202
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint EX202 (Flag: MySql). Message: " + dbException.Message);
                    this.Close();
                    return;
                }
                cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
            }
        }
        private void EX_Expense_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentExpense == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Validaciones.
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("La descripción del gasto está incompleta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Construye objeto "expense".
            var expense = new Expense()
            {
                ExpenseID = (CurrentExpense == null) ? 0 : CurrentExpense.ExpenseID,
                Date = dtpExpenseDate.Value,
                CategoryID = (int)cboCategory.SelectedValue,
                SubcategoryID = (int)cboSubcategory.SelectedValue,
                Description = txtDescription.Text,
                Amount = nudAmount.Value,
                CurrencyID = (int)cboCurrency.SelectedValue,
                InvoiceNumber = txtInvoiceNumber.Text.NullIfEmpty()
            };
            if (CurrentExpense == null)
            {
                try
                {
                    await Task.Run(() => expense.Insert());
                    MessageBox.Show("Gasto registrado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint EX203
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint EX203 (Flag: MySql). Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() => expense.Update());
                    MessageBox.Show("Gasto actualizado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint EX204
                    MessageBox.Show("Error en servidor MySql."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint EX204 (Flag: MySql). Message: " + dbException.Message);
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
                // Waypoint EX205
                MessageBox.Show("Error en servidor MySql."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint EX205 (Flag: MySql). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }
    }
}
