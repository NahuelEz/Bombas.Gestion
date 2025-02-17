using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PO_Items_Input_InputSearcher : Form
    {
        public int SelectedInputID;

        public PO_Items_Input_InputSearcher()
        {
            InitializeComponent();
            dgvSearchResults.AutoGenerateColumns = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // Validaciones.
            string pattern = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(pattern))
            {
                MessageBox.Show("Por favor, ingrese búsqueda.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                dgvSearchResults.DataSource = await Task.Run(() => Input.GetInputsByDescription(pattern));
                if (((List<Input>)dgvSearchResults.DataSource).Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception dbException)
            {
                // Waypoint PO501
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PO501 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }

        private void dgvSearchResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvSearchResults.SelectedRows.Count == 0)
            {
                return;
            }
            SelectedInputID = ((Input)dgvSearchResults.SelectedRows[0].DataBoundItem).InputID;
            this.DialogResult = DialogResult.OK;
        }
    }
}
