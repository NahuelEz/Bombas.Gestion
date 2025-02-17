using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Clover.DbLayer;
using System.Collections.Generic;

namespace Clover.Gestion
{
    public partial class AgregarCompraForm : Form
    {
        private int? historialId;
        private int leadId;

        private Dictionary<string, decimal> productosConPrecios = new Dictionary<string, decimal>();

        public AgregarCompraForm(int leadId, int? historialId = null)
        {
            InitializeComponent();
            this.historialId = historialId;
            this.leadId = leadId;

            ConfigurarControles();

            CargarProductos();
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;

            if (historialId.HasValue)
            {
                Text = "Editar Compra";
                CargarCompra(historialId.Value);
            }
            else
            {
                Text = "Agregar Compra";
            }
        }

        private void ConfigurarControles()
        {
            nudPrecio.Minimum = 0;
            nudPrecio.Maximum = 100000; // Ajusta este valor según tus necesidades
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Enabled = true;
            nudPrecio.Visible = true;
        }

        private void CargarProductos()
        {
            string query = "SELECT PartCode, Description, UnitPrice FROM product";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string partCode = reader["PartCode"].ToString();
                            string description = reader["Description"].ToString();
                            decimal unitPrice = Convert.ToDecimal(reader["UnitPrice"]);

                            string displayValue = $"{partCode} - {description}";
                            cmbProducto.Items.Add(displayValue);

                            // Agregar al diccionario
                            productosConPrecios[displayValue] = unitPrice;
                        }
                    }
                }
            }
        }

        private void CargarCompra(int historialId)
        {
            string query = @"SELECT Producto, Cantidad, Precio, Total, Notas, FechaCompra
                             FROM historialcompras
                             WHERE HistorialID = @HistorialID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HistorialID", historialId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbProducto.Text = reader["Producto"].ToString();
                            nudCantidad.Value = Convert.ToDecimal(reader["Cantidad"]);
                            nudPrecio.Value = Convert.ToDecimal(reader["Precio"]);
                            txtNotas.Text = reader["Notas"].ToString();
                        }
                    }
                }
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null)
            {
                string productoSeleccionado = cmbProducto.SelectedItem.ToString(); // Recupera el texto seleccionado

                if (productosConPrecios.TryGetValue(productoSeleccionado, out decimal precio)) // Busca el precio en el diccionario
                {
                    if (precio < nudPrecio.Minimum)
                    {
                        precio = nudPrecio.Minimum;
                    }
                    else if (precio > nudPrecio.Maximum)
                    {
                        precio = nudPrecio.Maximum;
                    }

                    nudPrecio.Value = precio; // Asigna el precio al control
                }
                else
                {
                    MessageBox.Show("No se encontró el precio para el producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                if (historialId.HasValue)
                {
                    ActualizarCompra();
                }
                else
                {
                    InsertarCompra();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertarCompra()
        {
            string query = @"INSERT INTO historialcompras (LeadID, Producto, Cantidad, Precio, Notas, FechaCompra)
                     VALUES (@LeadID, @Producto, @Cantidad, @Precio, @Notas, @FechaCompra)";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    cmd.Parameters.AddWithValue("@Producto", cmbProducto.Text);
                    cmd.Parameters.AddWithValue("@Cantidad", nudCantidad.Value);
                    cmd.Parameters.AddWithValue("@Precio", nudPrecio.Value);
                    cmd.Parameters.AddWithValue("@Notas", txtNotas.Text);
                    cmd.Parameters.AddWithValue("@FechaCompra", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ActualizarCompra()
        {
            string query = @"UPDATE historialcompras
                     SET Producto = @Producto,
                         Cantidad = @Cantidad,
                         Precio = @Precio,
                         Notas = @Notas
                     WHERE HistorialID = @HistorialID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Producto", cmbProducto.Text);
                    cmd.Parameters.AddWithValue("@Cantidad", nudCantidad.Value);
                    cmd.Parameters.AddWithValue("@Precio", nudPrecio.Value);
                    cmd.Parameters.AddWithValue("@Notas", txtNotas.Text);
                    cmd.Parameters.AddWithValue("@HistorialID", historialId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(cmbProducto.Text))
            {
                MessageBox.Show("El campo Producto es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudPrecio.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AgregarCompraForm_Load(object sender, EventArgs e)
        {

        }
    }
}
