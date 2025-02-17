using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class FormIngresoEgreso : Form
    {
        public decimal Monto { get; private set; }
        public string Metodo { get; private set; }

        public FormIngresoEgreso(string tipoTransaccion)
        {
            InitializeComponent();
            this.Text = $"Registrar {tipoTransaccion}";

            // Llenar el ComboBox con los métodos de pago disponibles
            cboMetodo.DataSource = Enum.GetNames(typeof(PaymentMethod));
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Monto = nudMonto.Value;
            Metodo = cboMetodo.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void FormIngresoEgreso_Load(object sender, EventArgs e)
        {

        }
    }
}