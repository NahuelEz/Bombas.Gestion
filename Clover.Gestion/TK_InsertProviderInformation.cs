using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class TK_InsertProviderInformation : Form
    {
        public string Output = null;

        public TK_InsertProviderInformation()
        {
            InitializeComponent();
        }

        private async void TK_InsertProviderInformation_Load(object sender, EventArgs e)
        {
            try
            {
                cboProvider.DisplayMember = "ProviderName";
                cboProvider.ValueMember = "ProviderID";
                cboProvider.DataSource = await Task.Run(() => Provider.GetProvidersLight());
                if (((List<Provider>)cboProvider.DataSource).Count == 0)
                {
                    MessageBox.Show("No hay proveedores registrados.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            catch (Exception dbException)
            {
                // Waypoint TK401
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK401 (Flag: MySQL). Message: " + dbException.Message);
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
            }
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

        private async void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica que haya un proveedor seleccionado.
            if (cboProvider.SelectedItem == null)
            {
                return;
            }

            int selectedProviderId = (int)cboProvider.SelectedValue;
            Provider provider = null;
            try
            {
                provider = await Task.Run(() => Provider.GetProviderById(selectedProviderId));
                cboContact.DisplayMember = "ContactName";
                cboContact.ValueMember = "ContactID";
                cboContact.DataSource = await Task.Run(() => ProviderContact.GetContactsByProviderId(selectedProviderId));
            }
            catch (Exception dbException)
            {
                // Waypoint TK402
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint TK402 (Flag: MySQL). Message: " + dbException.Message);
                this.Close();
                return;
            }
            string formattedAddress = string.IsNullOrWhiteSpace(provider.Address) ? "< No registrado >" :
                        $"{provider.Address}, {provider.City}, {provider.District}, {provider.Country}";
            lblAddress.Text = formattedAddress;

            if (cboContact.SelectedItem == null)
            {
                lblPhone.Text = string.Empty;
                lblSecondaryPhone.Text = string.Empty;
                lblEmail.Text = string.Empty;
            }
        }
        private void cboProvider_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Borra los campos si el proveedor ingresado no pertenece a la lista.
            if (cboProvider.SelectedItem == null)
            {
                cboProvider.Text = string.Empty;
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

            var selectedContact = (ProviderContact)cboContact.SelectedItem;
            lblPhone.Text = selectedContact.Phone;
            lblSecondaryPhone.Text = string.IsNullOrWhiteSpace(selectedContact.SecondaryPhone) ? "< No registrado >" : selectedContact.SecondaryPhone;
            lblEmail.Text = selectedContact.Email;
        }
    }
}
