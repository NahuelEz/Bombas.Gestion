using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clover.DbLayer;
using Clover.Shared;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    public partial class FormRegistroOrdenSellos : Form
    {
        public FormRegistroOrdenSellos()
        {
            InitializeComponent();
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormRegistroOrdenSellos_Load(object sender, EventArgs e)
        {
            txtIdPresupuesto.Text = GenerarNuevoID();
        }

        private string GenerarNuevoID()
        {
            // Implementa la lógica para generar un ID único.
            return Guid.NewGuid().ToString(); // O un número secuencial, según necesites.
        }

        private void cboMotorTypesellos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoMotor = cboMotorTypesellos.SelectedItem.ToString();

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void txtCableLenght_TextChanged(object sender, EventArgs e)
        {
            string materialCable = txtCableLenghtsellos.Text;

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void cboSealsDeliveredTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string resorteIzquierdo = cboSealsDeliveredTosellos.SelectedItem.ToString();

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            string tipoOringFuelle = rbnIsThreePhasesellos.Checked ? "O-Rings" : "Fuelles";

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string elastomero = comboBox4.SelectedItem.ToString();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pnlScrollableContainersellos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMakePDF_Click(object sender, EventArgs e)
        {
           
        }

        private void txtRepairOrderIDsellos_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRefNumbersellos_TextChanged(object sender, EventArgs e)
        {
            string numeroReferencia = txtRefNumbersellos.Text;

        }

        private void dtpDatesellos_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha = dtpDatesellos.Value;

        }

        private void cboCustomersellos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumbersellos_TextChanged(object sender, EventArgs e)
        {
            string telefono = txtPhoneNumbersellos.Text;

        }

        private void txtDeliveryNoteNumberCustomersellos_TextChanged(object sender, EventArgs e)
        {
            string numeroRemitoRecepcion = txtDeliveryNoteNumberCustomersellos.Text;

        }

        private void txtDeliveryNoteNumbersellos_TextChanged(object sender, EventArgs e)
        {
            string numeroRemitoEntregas = txtDeliveryNoteNumbersellos.Text;

        }

        private void txtInvoiceNumbersellos_TextChanged(object sender, EventArgs e)
        {
            string numeroFactura = txtInvoiceNumbersellos.Text;

        }

        private void txtPumpBrandsellos_TextChanged(object sender, EventArgs e)
        {
            string marca = txtPumpBrandsellos.Text;

        }

        private void txtSealssellos_TextChanged(object sender, EventArgs e)
        {
            string modeloSellos = txtSealssellos.Text;

        }

        private void comboBox1sellos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string resorteDerecho = comboBox1sellos.SelectedItem.ToString();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pistaRotativa = comboBox2.SelectedItem.ToString();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pistaEstacionaria = comboBox3.SelectedItem.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string materialEstacionario = textBox1.Text;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string materialOringFuelle = textBox2.Text;

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            bool limpiezaUltrasonido = radioButton1.Checked;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string diametroAlambre = textBox3.Text;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string vuelta = textBox4.Text;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string paso = textBox5.Text;

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string conexionDiametroInt = textBox6.Text;

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int rpm = int.Parse(textBox7.Text);

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string ranuraAltura = textBox8.Text;

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string hp = textBox9.Text;

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            string amperaje = textBox10.Text;

        }

        private void txtNotessellos_TextChanged(object sender, EventArgs e)
        {
            string otros = txtNotessellos.Text;

        }

        private void btnAcceptsellos_Click(object sender, EventArgs e)
        {
            try
            {
                // Llama al método que contiene la lógica para insertar los datos en la base de datos.
                GuardarDatos();
                MessageBox.Show("Los datos se han guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarDatos()
        {
            // Usa la cadena de conexión de DbLayerSettings en lugar de una cadena estática
            

            string query = "INSERT INTO ordenes_reparacion_sellos " +
                           "(id_presupuesto, fecha, cliente, marca, tipo, estado, numero_remito, numero_factura, prioridad, " +
                           "ref_numero, telefono, numero_remito_recepcion, numero_remito_entregas, motor_tipo, modelo_seal, " +
                           "resorte_izquierdo, resorte_derecho, pista_rotativa, material_cable, pista_estacionaria, material_estacionario, " +
                           "elastomero, tipo_orings_fuelles, material_orings_fuelles, limpieza_ultrasonido, diametro_alambre, vuelta, " +
                           "paso, conexion_diametro_int, rpm, ranura_altura, hp, amperaje, otros) " +
                           "VALUES (@id_presupuesto, @fecha, @cliente, @marca, @tipo, @estado, @numero_remito, @numero_factura, @prioridad, " +
                           "@ref_numero, @telefono, @numero_remito_recepcion, @numero_remito_entregas, @motor_tipo, @modelo_seal, " +
                           "@resorte_izquierdo, @resorte_derecho, @pista_rotativa, @material_cable, @pista_estacionaria, @material_estacionario, " +
                           "@elastomero, @tipo_orings_fuelles, @material_orings_fuelles, @limpieza_ultrasonido, @diametro_alambre, @vuelta, " +
                           "@paso, @conexion_diametro_int, @rpm, @ranura_altura, @hp, @amperaje, @otros)";

            
        }





    }
}
