using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Clover.DbLayer;

namespace Clover.Gestion
{
    public partial class CierreForm : Form
    {
        private int leadId;

        public CierreForm(int leadId)
        {
            InitializeComponent();
            this.leadId = leadId;
            ConfigurarFormulario();
            CargarProductosDesdeHistorial(); // Nueva función para cargar productos
            CargarDatosLead();
        }

        private void ConfigurarFormulario()
        {
            // Configuración inicial
            cmbEstadoCliente.Items.AddRange(new string[] { "Satisfecho", "Insatisfecho", "Pendiente" });
            cmbTipoFacturacion.Items.AddRange(new string[] { "A", "B", "C", "N" });
            cmbFormaPago.Items.AddRange(new string[] { "Transferencia", "Efectivo", "Tarjeta" });

            // Configurar controles
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 1000;
            nudCantidad.DecimalPlaces = 0;

            // Configurar ComboBox de Producto como desplegable
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarProductosDesdeHistorial()
        {
            // Consulta SQL para obtener los productos desde el historial de compras
            string query = @"SELECT DISTINCT Producto 
                             FROM historialcompras 
                             WHERE LeadID = @LeadID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbProducto.Items.Add(reader["Producto"].ToString());
                        }
                    }
                }
            }

            // Si no hay productos en el historial, deshabilitar el ComboBox
            if (cmbProducto.Items.Count == 0)
            {
                cmbProducto.Enabled = false;
                MessageBox.Show("No hay productos en el historial de compras de este cliente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cmbProducto.SelectedIndex = 0; // Seleccionar automáticamente el primer producto
            }
        }

        private void CargarDatosLead()
        {
            // Cargar datos básicos del lead si ya existe
            string query = @"SELECT EstadoCliente, NotaCliente, Cantidad, Producto, TipoFacturacion, FormaPago, NotaEntrega, OrdenCompra, 
                             PrepararPedido, Facturar, Cobrar, Despachar 
                             FROM LeadsCierre WHERE LeadID = @LeadID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbEstadoCliente.Text = reader["EstadoCliente"].ToString();
                            txtNotaCliente.Text = reader["NotaCliente"].ToString();
                            nudCantidad.Value = Convert.ToDecimal(reader["Cantidad"]);
                            cmbProducto.Text = reader["Producto"].ToString();
                            cmbTipoFacturacion.Text = reader["TipoFacturacion"].ToString();
                            cmbFormaPago.Text = reader["FormaPago"].ToString();
                            txtNotaEntrega.Text = reader["NotaEntrega"].ToString();
                            txtOrdenCompra.Text = reader["OrdenCompra"].ToString();
                            chkPrepararPedido.Checked = Convert.ToBoolean(reader["PrepararPedido"]);
                            chkFacturar.Checked = Convert.ToBoolean(reader["Facturar"]);
                            chkCobrar.Checked = Convert.ToBoolean(reader["Cobrar"]);
                            chkDespachar.Checked = Convert.ToBoolean(reader["Despachar"]);
                        }
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                GuardarCierre();
                MessageBox.Show("Cierre guardado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cierre: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(cmbEstadoCliente.Text))
            {
                MessageBox.Show("El campo Estado del Cliente es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbProducto.Text))
            {
                MessageBox.Show("El campo Producto es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbTipoFacturacion.Text))
            {
                MessageBox.Show("Debe seleccionar un tipo de facturación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void GuardarCierre()
        {
            string query = @"
    INSERT INTO LeadsCierre (LeadID, EstadoCliente, NotaCliente, Cantidad, Producto, TipoFacturacion, FormaPago, NotaEntrega, 
    OrdenCompra, PrepararPedido, Facturar, Cobrar, Despachar, FechaCierre) 
    VALUES (@LeadID, @EstadoCliente, @NotaCliente, @Cantidad, @Producto, @TipoFacturacion, @FormaPago, @NotaEntrega, 
    @OrdenCompra, @PrepararPedido, @Facturar, @Cobrar, @Despachar, @FechaCierre)
    ON DUPLICATE KEY UPDATE EstadoCliente = @EstadoCliente, NotaCliente = @NotaCliente, Cantidad = @Cantidad, 
    Producto = @Producto, TipoFacturacion = @TipoFacturacion, FormaPago = @FormaPago, NotaEntrega = @NotaEntrega, 
    OrdenCompra = @OrdenCompra, PrepararPedido = @PrepararPedido, Facturar = @Facturar, Cobrar = @Cobrar, 
    Despachar = @Despachar, FechaCierre = @FechaCierre;

    UPDATE Leads
    SET Estado = 'Cerrado'
    WHERE LeadID = @LeadID;";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    cmd.Parameters.AddWithValue("@EstadoCliente", cmbEstadoCliente.Text);
                    cmd.Parameters.AddWithValue("@NotaCliente", txtNotaCliente.Text);
                    cmd.Parameters.AddWithValue("@Cantidad", nudCantidad.Value);
                    cmd.Parameters.AddWithValue("@Producto", cmbProducto.Text);
                    cmd.Parameters.AddWithValue("@TipoFacturacion", cmbTipoFacturacion.Text);
                    cmd.Parameters.AddWithValue("@FormaPago", cmbFormaPago.Text);
                    cmd.Parameters.AddWithValue("@NotaEntrega", txtNotaEntrega.Text);
                    cmd.Parameters.AddWithValue("@OrdenCompra", txtOrdenCompra.Text);
                    cmd.Parameters.AddWithValue("@PrepararPedido", chkPrepararPedido.Checked);
                    cmd.Parameters.AddWithValue("@Facturar", chkFacturar.Checked);
                    cmd.Parameters.AddWithValue("@Cobrar", chkCobrar.Checked);
                    cmd.Parameters.AddWithValue("@Despachar", chkDespachar.Checked);
                    cmd.Parameters.AddWithValue("@FechaCierre", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
