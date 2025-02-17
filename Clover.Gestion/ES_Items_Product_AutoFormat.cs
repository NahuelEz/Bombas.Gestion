using System;
using System.Linq;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ES_Items_Product_AutoFormat : Form
    {
        public string FormattedDescription = null;
        private string PartCode = null;
        private string TypeDescription = null;

        private string[] stationaryMaterials = { "Carbón", "Cerámica", "Silicio", "Tungsteno", "Stellite", "Inox.", "Bronce", "Teflón con carga", "Ni-resist" };
        private string[] rotativeMaterials = { "Carbón", "Cerámica", "Silicio", "Tungsteno", "Stellite", "Inox.", "Bronce", "Teflón con carga", "Ni-resist" };
        private string[] elastomerMaterials = { "Nitrilo", "EPDM", "Viton", "Kalrez", "Viton Extreme", "Aflas", "Cloropreno", "FEP", "Teflón", "Buna", "Chemraz", "Silicona" };

        public ES_Items_Product_AutoFormat(string PartCode, string TypeDescription)
        {
            this.PartCode = PartCode;
            this.TypeDescription = TypeDescription;
            InitializeComponent();
            cboStationary.DataSource = stationaryMaterials;
            cboRotative.DataSource = rotativeMaterials;
            cboElastomers.DataSource = elastomerMaterials;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            string template = "Sello mecánico tipo {0}. Modelo: {1}, para eje de {2}."
                            + Environment.NewLine + "Materiales: la pista estacionaria es de {3}, la pista rotativa es de {4}, los elastómeros son de {5} y las demás partes de acero inoxidable.";
            string customPartCode = PartCode + cboRotative.Text.First() + cboStationary.Text.First() + cboElastomers.Text.First() + txtDiameter.Text;
            FormattedDescription = string.Format(template,
                TypeDescription,
                customPartCode,
                txtDiameter.Text + (rbnMetricDiameter.Checked ? " mm" : "\""),
                cboStationary.Text,
                cboRotative.Text,
                cboElastomers.Text);
            DialogResult = DialogResult.OK;
        }
    }
}
