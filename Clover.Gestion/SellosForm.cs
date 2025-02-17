using System;
using System.Windows.Forms;
using Clover.DbLayer;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    public partial class SellosForm : Form
    {
        public SellosForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(SellosForm_Load);
        }

        private void SellosForm_Load(object sender, EventArgs e)
        {
            // Mostrar un indicador de carga mientras se establecen las conexiones
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // Cargar los clientes en el ComboBox de manera sincrónica
                CargarClientes();
            }
            finally
            {
                // Restaurar el cursor una vez que las operaciones terminen
                Cursor.Current = Cursors.Default;
            }
        }

        private void CargarClientes()
        {
            string connectionString = DbLayerSettings.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }

            string query = "SELECT CustomerName FROM customer";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open(); // Abrir conexión de manera sincrónica

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cboCustomersellos.Items.Add(reader["CustomerName"].ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                // Manejar errores silenciosamente si es necesario
            }
        }

        private void btnAcceptsellos_Click(object sender, EventArgs e)
        {
            try
            {
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
            string connectionString = DbLayerSettings.ConnectionString;

            string query = @"INSERT INTO SellosOrders 
        (RefNumber, OrderDate, Customer, PhoneNumber, DeliveryNoteNumberCustomer, 
        DeliveryNoteNumber, InvoiceNumber, MotorType, PumpBrand, Seal, SealDeliveredTo, 
        ComboBox1, PistaRotativa, Material, Pista, PistaMaterial, Elastomero, DiamAlambre, 
        Vueltas, Peso, Resortes, Seguers, Juntas, ClipsCentradores, SeguersMultiResorte, Notes)
        VALUES (@RefNumber, @OrderDate, @Customer, @PhoneNumber, @DeliveryNoteNumberCustomer, 
        @DeliveryNoteNumber, @InvoiceNumber, @MotorType, @PumpBrand, @Seal, @SealDeliveredTo, 
        @ComboBox1, @PistaRotativa, @Material, @Pista, @PistaMaterial, @Elastomero, @DiamAlambre, 
        @Vueltas, @Peso, @Resortes, @Seguers, @Juntas, @ClipsCentradores, @SeguersMultiResorte, @Notes)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open(); // Abrir conexión de manera sincrónica

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Asignar los valores reales de los controles del formulario
                        cmd.Parameters.AddWithValue("@RefNumber", txtRefNumbersellos.Text);
                        cmd.Parameters.AddWithValue("@OrderDate", dtpDatesellos.Value);
                        cmd.Parameters.AddWithValue("@Customer", cboCustomersellos.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumbersellos.Text);
                        cmd.Parameters.AddWithValue("@DeliveryNoteNumberCustomer", txtDeliveryNoteNumberCustomersellos.Text);
                        cmd.Parameters.AddWithValue("@DeliveryNoteNumber", txtDeliveryNoteNumbersellos.Text);
                        cmd.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNumbersellos.Text);
                        cmd.Parameters.AddWithValue("@MotorType", cboMotorTypesellos.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@PumpBrand", txtPumpBrandsellos.Text);
                        cmd.Parameters.AddWithValue("@Seal", txtSealssellos.Text);
                        cmd.Parameters.AddWithValue("@SealDeliveredTo", cboSealsDeliveredTosellos.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@ComboBox1", comboBox1sellos.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@PistaRotativa", cbopistarotativa.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Material", txtmaterialsellos.Text);
                        cmd.Parameters.AddWithValue("@Pista", comboBoxpistasellos.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@PistaMaterial", textboxpistamatsellos.Text);
                        cmd.Parameters.AddWithValue("@Elastomero", comboBoxelastomero.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@DiamAlambre", textBoxdiamalambre.Text);
                        cmd.Parameters.AddWithValue("@Vueltas", textBoxvueltasellos.Text);
                        cmd.Parameters.AddWithValue("@Peso", textBoxpesosellos.Text);

                        // ComboBox añadidos
                        cmd.Parameters.AddWithValue("@Resortes", comboBoxResortes.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Seguers", comboBox2.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Juntas", comboBoxJuntas1.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@ClipsCentradores", comboBoxClipsCentradores.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@SeguersMultiResorte", comboBoxMultiResorteSeguers.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Notes", txtNotessellos.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Error de MySQL: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void SellosForm_Load_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
