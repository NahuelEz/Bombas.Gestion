using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PR_ProductHistory_DetailedHistory : Form
    {
        private int ProductID;
        private int CustomerID;
        private string PartCode;
        private string CustomerName;

        public PR_ProductHistory_DetailedHistory(int ProductID, int CustomerID, string PartCode, string CustomerName)
        {
            this.ProductID = ProductID;
            this.CustomerID = CustomerID;
            this.PartCode = PartCode;
            this.CustomerName = CustomerName;
            InitializeComponent();
            dgvSales.AutoGenerateColumns = false;
        }

        private async void CU_CustomerProductHistory_Load(object sender, EventArgs e)
        {
            this.Text = $"Historial de ventas: {PartCode} / {CustomerName}";
            lblDescription.Text = $"{PartCode} / {CustomerName}";
            try
            {
                dgvSales.DataSource = await Task.Run(() => Product.GetSalesFromProduct(ProductID, CustomerID));
            }
            catch (Exception dbException)
            {
                // Waypoint PR801
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PR801 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnGoToTransaction_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("No hay ninguna operación seleccionada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedSale = (Sale)dgvSales.SelectedRows[0].DataBoundItem;
            using (var form = new SA_Sale(selectedSale.SaleID, SAParameterType.SaleID))
            {
                form.ShowDialog();
            }
        }
    }
}
