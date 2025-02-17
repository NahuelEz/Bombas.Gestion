using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PR_ProductHistory : Form
    {
        private int ProductID;
        private string PartCode;

        public PR_ProductHistory(int ProductID, string PartCode)
        {
            this.ProductID = ProductID;
            this.PartCode = PartCode;
            InitializeComponent();
            dgvCustomers.AutoGenerateColumns = false;
        }

        private async void PR_ProductHistory_Load(object sender, EventArgs e)
        {
            this.Text = $"Historial de ventas: {PartCode}";
            lblPartCode.Text = PartCode;
            try
            {
                dgvCustomers.DataSource = await Task.Run(() => Product.GetBuyersFromProduct(ProductID));
            }
            catch (Exception dbException)
            {
                // Waypoint PR701
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PR701 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnShowDetailedHistory_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ningún cliente seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
            using (var form = new PR_ProductHistory_DetailedHistory(ProductID, selectedCustomer.CustomerID, PartCode, selectedCustomer.CustomerName))
            {
                form.ShowDialog();
            }
        }
    }
}
