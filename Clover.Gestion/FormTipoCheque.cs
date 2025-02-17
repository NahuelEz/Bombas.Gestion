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
    public partial class FormTipoCheque : Form
    {
        public DateTime? FechaCobro { get; private set; } // Para almacenar la fecha seleccionada si es Cheque Diferido
        public string TipoCheque { get; private set; }    // Para almacenar el tipo de cheque seleccionado

        public FormTipoCheque()
        {
            InitializeComponent();
        }

        private void rbChequeDiferido_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaCobro.Visible = rbChequeDiferido.Checked; // Mostrar u ocultar el calendario según el tipo de cheque
        }

        private void rbChequeComun_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaCobro.Visible = false; // Ocultar el calendario si se selecciona Cheque Común
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificar qué tipo de cheque fue seleccionado
            if (rbChequeComun.Checked)
            {
                TipoCheque = "Cheque Común";
                FechaCobro = null; // No hay fecha de cobro para cheque común
            }
            else if (rbChequeDiferido.Checked)
            {
                TipoCheque = "Cheque Diferido";
                FechaCobro = dtpFechaCobro.Value; // Capturar la fecha seleccionada
            }

            this.DialogResult = DialogResult.OK; // Cerrar el formulario y retornar al formulario principal
            this.Close();
        }
    }

}
