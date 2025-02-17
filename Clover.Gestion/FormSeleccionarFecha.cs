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
    public partial class FormSeleccionarFecha : Form
    {

        // Propiedad pública para almacenar la fecha seleccionada
        public DateTime FechaSeleccionada { get; private set; }

        public FormSeleccionarFecha()
        {
            InitializeComponent();
            // Configurar el DateTimePicker con una fecha por defecto
            dateTimePickerFecha.Value = DateTime.Today;
        }

        // Este método se ejecuta cuando el usuario hace clic en el botón "Confirmar"
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Guardar la fecha seleccionada en la propiedad FechaSeleccionada
            FechaSeleccionada = dateTimePickerFecha.Value.Date; // Aquí obtenemos la fecha del DateTimePicker
            DialogResult = DialogResult.OK; // Indica que el usuario ha confirmado
            this.Close(); // Cierra el formulario
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
