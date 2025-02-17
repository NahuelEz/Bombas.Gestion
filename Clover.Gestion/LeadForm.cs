using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Clover.DbLayer;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    public partial class LeadForm : Form
    {
        public int? LeadID { get; set; }  // ID del lead (nulo si es nuevo)
        private bool isEditMode;  // Modo de edición

        public LeadForm(int? leadId = null)
        {
            InitializeComponent();
            LeadID = leadId;
            isEditMode = leadId.HasValue;

            ConfigurarHistorialGrid();

            if (isEditMode)
            {
                LoadLeadData(leadId.Value);
                CargarHistorial(leadId.Value);
            }
        }

        private void LoadLeadData(int leadId)
        {
            string query = "SELECT * FROM Leads WHERE LeadID = @LeadID";

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
                            // Cargar los valores en los controles del formulario
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtApellido.Text = reader["Apellido"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtEmpresa.Text = reader["Empresa"].ToString();
                            txtCuitCuil.Text = reader["CuitCuil"].ToString();
                            cmbNivelUrgencia.Text = reader["NivelUrgencia"].ToString();
                            cmbMedioContacto.Text = reader["MedioContacto"].ToString();
                            cmbFormaPago.Text = reader["FormaPago"].ToString();
                            txtInteres.Text = reader["Interes"].ToString();
                            txtEspecificacion.Text = reader["Especificacion"].ToString();
                            txtProducto.Text = reader["Producto"].ToString();
                            txtTipoProducto.Text = reader["TipoProducto"].ToString();
                            txtInstalacion.Text = reader["Instalacion"].ToString();
                            txtCantidad.Text = reader["Cantidad"].ToString();
                            txtReferencias.Text = reader["Referencias"].ToString();
                            txtNota.Text = reader["Nota"].ToString();
                            cmbEstado.Text = reader["Estado"].ToString();
                        }
                    }
                }
            }
        }

        private void ConfigurarHistorialGrid()
        {
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.RowHeadersVisible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return; // No procede si hay errores en los campos
            }

            try
            {
                if (isEditMode)
                {
                    UpdateLead();
                }
                else
                {
                    InsertLead();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el lead: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertLead()
        {
            string query = @"INSERT INTO Leads (Nombre, Apellido, Telefono, Email, Empresa, CuitCuil, NivelUrgencia, MedioContacto, FormaPago, 
                            Interes, Especificacion, Producto, TipoProducto, Instalacion, Cantidad, Referencias, Nota, Estado)
                            VALUES (@Nombre, @Apellido, @Telefono, @Email, @Empresa, @CuitCuil, @NivelUrgencia, @MedioContacto, @FormaPago, 
                            @Interes, @Especificacion, @Producto, @TipoProducto, @Instalacion, @Cantidad, @Referencias, @Nota, @Estado)";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    SetParameters(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateLead()
        {
            string query = @"UPDATE Leads SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono, Email=@Email, Empresa=@Empresa, 
                            CuitCuil=@CuitCuil, NivelUrgencia=@NivelUrgencia, MedioContacto=@MedioContacto, FormaPago=@FormaPago, 
                            Interes=@Interes, Especificacion=@Especificacion, Producto=@Producto, TipoProducto=@TipoProducto, 
                            Instalacion=@Instalacion, Cantidad=@Cantidad, Referencias=@Referencias, Nota=@Nota, Estado=@Estado 
                            WHERE LeadID=@LeadID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", LeadID);

                    // Registrar cambios importantes en el historial
                    RegistrarHistorial(LeadID.Value, "Estado", cmbEstado.Text, "NuevoValor"); // Cambiar según corresponda

                    SetParameters(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void RegistrarHistorial(int leadId, string campo, string valorAnterior, string valorNuevo)
        {
            string query = @"INSERT INTO LeadHistorial (LeadID, CampoModificado, ValorAnterior, ValorNuevo, Usuario) 
                             VALUES (@LeadID, @Campo, @ValorAnterior, @ValorNuevo, @Usuario)";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    cmd.Parameters.AddWithValue("@Campo", campo);
                    cmd.Parameters.AddWithValue("@ValorAnterior", valorAnterior);
                    cmd.Parameters.AddWithValue("@ValorNuevo", valorNuevo);
                    cmd.Parameters.AddWithValue("@Usuario", "Sistema"); // Cambiar por usuario real si aplica
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CargarHistorial(int leadId)
        {
            string query = @"SELECT FechaCambio AS 'Fecha de Cambio', 
                                    CampoModificado AS 'Campo Modificado', 
                                    ValorAnterior AS 'Valor Anterior', 
                                    ValorNuevo AS 'Valor Nuevo', 
                                    Usuario 
                             FROM LeadHistorial 
                             WHERE LeadID = @LeadID 
                             ORDER BY FechaCambio DESC";

            DataTable historial = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(historial);
                    }
                }
            }

            // Asigna los datos al DataGridView
            dgvHistorial.DataSource = historial;
        }

        private void SetParameters(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Empresa", txtEmpresa.Text);
            cmd.Parameters.AddWithValue("@CuitCuil", txtCuitCuil.Text);
            cmd.Parameters.AddWithValue("@NivelUrgencia", cmbNivelUrgencia.Text);
            cmd.Parameters.AddWithValue("@MedioContacto", cmbMedioContacto.Text);
            cmd.Parameters.AddWithValue("@FormaPago", cmbFormaPago.Text);
            cmd.Parameters.AddWithValue("@Interes", txtInteres.Text);
            cmd.Parameters.AddWithValue("@Especificacion", txtEspecificacion.Text);
            cmd.Parameters.AddWithValue("@Producto", txtProducto.Text);
            cmd.Parameters.AddWithValue("@TipoProducto", txtTipoProducto.Text);
            cmd.Parameters.AddWithValue("@Instalacion", txtInstalacion.Text);
            cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
            cmd.Parameters.AddWithValue("@Referencias", txtReferencias.Text);
            cmd.Parameters.AddWithValue("@Nota", txtNota.Text);
            cmd.Parameters.AddWithValue("@Estado", cmbEstado.Text);
        }

        private bool ValidarCampos()
        {
            // Validar Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Apellido
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El campo Apellido es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Teléfono
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("El campo Teléfono debe contener solo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("El campo Email debe ser válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Estado
            if (string.IsNullOrWhiteSpace(cmbEstado.Text))
            {
                MessageBox.Show("Debe seleccionar un Estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Nivel de Urgencia
            if (string.IsNullOrWhiteSpace(cmbNivelUrgencia.Text))
            {
                MessageBox.Show("Debe seleccionar un Nivel de Urgencia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Medio de Contacto
            if (string.IsNullOrWhiteSpace(cmbMedioContacto.Text))
            {
                MessageBox.Show("Debe seleccionar un Medio de Contacto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Forma de Pago
            if (string.IsNullOrWhiteSpace(cmbFormaPago.Text))
            {
                MessageBox.Show("Debe seleccionar una Forma de Pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar CUIT/CUIL (opcional, si se ingresa)
            if (!string.IsNullOrWhiteSpace(txtCuitCuil.Text) && (!txtCuitCuil.Text.All(char.IsDigit) || txtCuitCuil.Text.Length != 11))
            {
                MessageBox.Show("El campo CUIT/CUIL debe tener 11 números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar Cantidad (opcional, si se ingresa)
            if (!string.IsNullOrWhiteSpace(txtCantidad.Text) && (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0))
            {
                MessageBox.Show("El campo Cantidad debe ser un número entero mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Si todas las validaciones son correctas
            return true;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
