using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class TK_InsertCustomerInformation : Form
    {
        public string Output = null;

        public TK_InsertCustomerInformation()
        {
            InitializeComponent();
        }

        private async void TK_InsertCustomerInformation_Load(object sender, EventArgs e)
        {
            try
            {
                cboCustomer.DisplayMember = "CustomerName";
                cboCustomer.ValueMember = "CustomerID";
                cboCustomer.DataSource = await Task.Run(() => Customer.GetCustomersLight());
                if (((List<Customer>)cboCustomer.DataSource).Count == 0)
                {
                    MessageBox.Show("No hay clientes registrados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            catch (Exception dbException)
            {
                // Waypoint TK501
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK501 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
        }

        private void lblAddress_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblAddress.Text))
            {
                Output = lblAddress.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
        private void lblPhone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblPhone.Text))
            {
                Output = lblPhone.Text;
                this.DialogResult = DialogResult.OK;
            };
        }
        private void lblSecondaryPhone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblSecondaryPhone.Text))
            {
                Output = lblSecondaryPhone.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
        private void lblEmail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lblEmail.Text))
            {
                Output = lblEmail.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica que haya un cliente seleccionado.
            if (cboCustomer.SelectedItem == null)
            {
                return;
            }

            int selectedCustomerId = (int)cboCustomer.SelectedValue;
            Customer customer = null;
            try
            {
                customer = await Task.Run(() => Customer.GetCustomerById(selectedCustomerId));
                cboContact.DisplayMember = "ContactName";
                cboContact.ValueMember = "ContactID";
                cboContact.DataSource = await Task.Run(() => CustomerContact.GetContactsByCustomerId(selectedCustomerId));
            }
            catch (Exception dbException)
            {
                // Waypoint TK502
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK502 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            string formattedAddress = string.IsNullOrWhiteSpace(customer.Address) ? "< No registrado >" :
                        $"{customer.Address}, {customer.City}, {customer.District}, {customer.Country}";
            lblAddress.Text = formattedAddress;

            if (cboContact.SelectedItem == null)
            {
                lblPhone.Text = string.Empty;
                lblSecondaryPhone.Text = string.Empty;
                lblEmail.Text = string.Empty;
            }
        }
        private void cboCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Borra los campos si el cliente ingresado no pertenece a la lista.
            if (cboCustomer.SelectedItem == null)
            {
                cboCustomer.Text = string.Empty;
                cboContact.DataSource = null;
                lblAddress.Text = string.Empty;
            }
        }
        private void cboContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica que haya un contacto seleccionado.
            if (cboContact.SelectedItem == null)
            {
                lblPhone.Text = string.Empty;
                lblSecondaryPhone.Text = string.Empty;
                lblEmail.Text = string.Empty;
                return;
            }

            var selectedContact = (CustomerContact)cboContact.SelectedItem;
            lblPhone.Text = selectedContact.Phone;
            lblSecondaryPhone.Text = string.IsNullOrWhiteSpace(selectedContact.SecondaryPhone) ? "< No registrado >" : selectedContact.SecondaryPhone;
            lblEmail.Text = selectedContact.Email;
        }
    }
}
