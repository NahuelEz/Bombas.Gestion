using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clover.DbLayer;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    public partial class FormCustomerPayments : Form
    {
        public FormCustomerPayments()
        {
            InitializeComponent();
            // Asegúrate de que el método LoadCustomerPayments se llame en el evento Load del formulario
            this.Load += FormCustomerPayments_Load;
        }

        private void FormCustomerPayments_Load(object sender, EventArgs e)
        {
            // Llamar al método para cargar los datos cuando se cargue el formulario
            LoadCustomerPayments();
        }

        private void LoadCustomerPayments()
        {
            // Eliminado el mensaje de depuración
            // string connectionString = DbLayerSettings.ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                try
                {
                    conn.Open();
                    // Eliminado el mensaje de conexión exitosa

                    string query = "SELECT CustomerPaymentID, CustomerID, AccountID, TotalAmount, FechaChequeDiferido " +
                                   "FROM customer_payment " +
                                   "WHERE AccountID = 3";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Verificar si el DataTable tiene filas (sin mensajes emergentes)
                    if (dataTable.Rows.Count > 0)
                    {
                        // Asegurarse de que AutoGenerateColumns esté activado
                        dgvCustomerPayments.AutoGenerateColumns = true;

                        // Asignar el DataSource al DataGridView
                        dgvCustomerPayments.DataSource = dataTable;

                        // Forzar el refresco del DataGridView
                        dgvCustomerPayments.Refresh();

                        // Cambiar los encabezados de las columnas
                        dgvCustomerPayments.Columns["CustomerPaymentID"].HeaderText = "ID de Pago";
                        dgvCustomerPayments.Columns["CustomerID"].HeaderText = "ID de Cliente";
                        dgvCustomerPayments.Columns["AccountID"].HeaderText = "ID de Cuenta";
                        dgvCustomerPayments.Columns["TotalAmount"].HeaderText = "Monto Total";
                        dgvCustomerPayments.Columns["FechaChequeDiferido"].HeaderText = "Fecha Cobro Cheque";

                        // Ajustar el modo de las columnas para que ocupen todo el ancho del DataGridView
                        dgvCustomerPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Ordenar por fecha de cheque diferido, de más reciente a más antiguo
                        dgvCustomerPayments.Sort(dgvCustomerPayments.Columns["FechaChequeDiferido"], ListSortDirection.Descending);

                        // Anclar el DataGridView a los cuatro bordes del formulario (izquierda, derecha, arriba, abajo)
                        dgvCustomerPayments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;


                    }
                    else
                    {
                        // Si lo deseas, podrías mostrar un mensaje si no se encuentran datos.
                        // Ejemplo: MessageBox.Show("No se encontraron datos para AccountID = 3.");
                    }
                }
                catch (MySqlException mysqlEx)
                {
                    // Manejo detallado de excepciones MySQL
                    // Puedes registrar el error o manejarlo de manera silenciosa si no deseas mostrar una ventana emergente.
                    // Ejemplo: Logger.AppendLog("MySQL Error: " + mysqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Manejo general de excepciones
                    // Ejemplo: Logger.AppendLog("Error general: " + ex.Message);
                }
            }
        }

        private void FormCustomerPayments_Load_1(object sender, EventArgs e)
        {

        }

        private void dgvCustomerPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
