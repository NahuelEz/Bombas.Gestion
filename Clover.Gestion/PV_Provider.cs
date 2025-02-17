using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class PV_Provider : Form
    {
        private int? ProviderID = null;
        private Provider CurrentProvider = null;
        private bool SafeExit = false;
        private List<ProviderContact> Contacts = new List<ProviderContact>();
        
        public PV_Provider(int? ProviderID = null)
        {
            this.ProviderID = ProviderID;
            InitializeComponent();
        }

        private async void PV_Provider_Load(object sender, EventArgs e)
        {
            // Rellena campos desplegables.
            try
            {
                cboTaxGroup.DataSource = new string[] { "Responsable Inscripto", "Monotributista", "Consumidor final", "IVA excento" };
                cboBusiness.DisplayMember = "BusinessName";
                cboBusiness.ValueMember = "BusinessID";
                cboBusiness.DataSource = await Task.Run(() => Business.GetBusinessLight());
                cboCountry.DisplayMember = "CountryName";
                cboCountry.ValueMember = "CountryID";
                cboCountry.DataSource = await Task.Run(() => Country.GetCountries());
                cboCountry.SelectedItem = null;
            }
            catch (Exception dbException)
            {
                // Waypoint PV201
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV201 (Flag: MySQL). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
            }
            if (ProviderID.HasValue)
            {
                try
                {
                    CurrentProvider = await Task.Run(() => Provider.GetProviderById(ProviderID.Value));
                    Contacts = await Task.Run(() => ProviderContact.GetContactsByProviderId(ProviderID.Value));
                }
                catch (Exception dbException)
                {
                    // Waypoint PV202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PV202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                }
                this.Text = "Visualizando proveedor: " + CurrentProvider.ProviderName;
                txtProviderID.Text = ProviderID.Value.ToString("D8");
                txtProviderName.Text = CurrentProvider.ProviderName;
                rbnIsCUIT.Checked = CurrentProvider.IsCUIT; rbnIsDNI.Checked = !CurrentProvider.IsCUIT;
                txtIdentityNumber.Text = CurrentProvider.IdentityNumber;
                cboTaxGroup.SelectedItem = CurrentProvider.TaxGroup;
                nudPaymentTerm.Value = CurrentProvider.PaymentTerm;
                cboBusiness.SelectedValue = CurrentProvider.BusinessID;
                dtpRegistryDate.Value = CurrentProvider.RegistryDate;
                if (!string.IsNullOrWhiteSpace(CurrentProvider.Address))
                {
                    if (CurrentProvider.CountryID == 9) // = Argentina
                    {
                        try
                        {
                            cboDistrict.DisplayMember = "DistrictName";
                            cboDistrict.ValueMember = "DistrictID";
                            cboDistrict.DataSource = await Task.Run(() => District.GetDistrictsByCountryID(9));
                            var locations = await Task.Run(() => City.GetCities(9, CurrentProvider.DistrictID.Value));
                            var citiesAutoCompleteSource = new AutoCompleteStringCollection();
                            citiesAutoCompleteSource.AddRange(locations.Select(l => $"{l.CityName} ({l.PostalCode})").ToArray());
                            txtCity.AutoCompleteCustomSource = citiesAutoCompleteSource;
                        }
                        catch (Exception dbException)
                        {
                            // Waypoint PV203
                            MessageBox.Show("Error en servidor MySQL."
                                + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.AppendLog("Exception at Waypoint PV203 (Flag: MySQL). Message: " + dbException.Message);
                            SafeExit = true;
                            this.Close();
                        }
                        cboCountry.SelectedValue = CurrentProvider.CountryID;
                        cboDistrict.SelectedValue = CurrentProvider.DistrictID;
                        cboDistrict.DropDownStyle = ComboBoxStyle.DropDownList;
                        txtCity.Text = CurrentProvider.City;
                        txtAddress.Text = CurrentProvider.Address;
                        // Activa funciones evento-controladas.
                        cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
                        cboDistrict.SelectedIndexChanged += cboDistrict_SelectedIndexChanged;
                        txtCity.Validating += txtCity_Validating;
                    }
                    else
                    {
                        cboCountry.SelectedValue = CurrentProvider.CountryID;
                        cboDistrict.Text = CurrentProvider.District;
                        txtCity.Text = CurrentProvider.City;
                        txtAddress.Text = CurrentProvider.Address;
                        // Activa funciones evento-controladas.
                        cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
                    }
                }
                else
                {
                    // Activa funciones evento-controladas.
                    cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
                }
            }
            else
            {
                // Activa funciones evento-controladas.
                cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
            }
        }
        private void PV_Provider_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentProvider == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            bool invalidName = string.IsNullOrWhiteSpace(txtProviderName.Text);
            bool invalidId = !Regex.IsMatch(txtIdentityNumber.Text, rbnIsCUIT.Checked ? @"^\d{2}\-\d{8}\-\d$" : @"^\d{7,8}$");
            // Muestra señal en campos inválidos.
            pnl_1.Visible = invalidName;
            pnl_2.Visible = invalidId;
            // Valida campos
            if (invalidName || invalidId)
            {
                MessageBox.Show("Uno o más campos no poseen el formato correcto o contienen caracteres no permitidos."
                    + Environment.NewLine + Environment.NewLine + "Revise la información ingresada.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool incompleteAddress = (string.IsNullOrWhiteSpace(cboCountry.Text) || string.IsNullOrWhiteSpace(cboDistrict.Text)
                                   || string.IsNullOrWhiteSpace(txtCity.Text) || string.IsNullOrWhiteSpace(txtAddress.Text));
            // Construye objeto "provider".
            var provider = new Provider();
            provider.ProviderID = (CurrentProvider == null) ? 0 : CurrentProvider.ProviderID;
            provider.ProviderName = txtProviderName.Text;
            provider.IdentityNumber = txtIdentityNumber.Text;
            provider.IsCUIT = rbnIsCUIT.Checked;
            provider.TaxGroup = cboTaxGroup.Text;
            provider.PaymentTerm = (int)nudPaymentTerm.Value;
            provider.BusinessID = (int)cboBusiness.SelectedValue;
            if (!incompleteAddress)
            {
                provider.CountryID = (int)cboCountry.SelectedValue;
                provider.Country = cboCountry.Text;
                provider.DistrictID = (int)cboDistrict.SelectedValue;
                provider.District = cboDistrict.Text;
                provider.City = txtCity.Text;
                provider.Address = txtAddress.Text;
            }
            else
            {
                var dialog = MessageBox.Show("La dirección no se guardará porque está incompleta. ¿Desea continuar?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog != DialogResult.OK)
                {
                    return;
                }
            }
            provider.RegistryDate = dtpRegistryDate.Value.Date;
            if (CurrentProvider == null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var transactionHandler = new DbTransactionHandler())
                        {
                            // Registra nuevo proveedor.
                            provider.Insert(transactionHandler);
                            // Registra contactos asociados.
                            foreach (var contact in Contacts)
                            {
                                contact.ProviderID = provider.ProviderID;
                                contact.Insert(transactionHandler);
                            }
                            transactionHandler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Proveedor registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint PV204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PV204 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
            else
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var transactionHandler = new DbTransactionHandler())
                        {
                            // Actualiza proveedor existente.
                            provider.Update(transactionHandler);
                            // Remueve contactos eliminados.
                            var oldContactsIds = ProviderContact.GetContactsByProviderId(provider.ProviderID, transactionHandler).Select(x => x.ContactID);
                            var newContactsIds = Contacts.Select(x => x.ContactID);
                            foreach (int contactId in oldContactsIds.Except(newContactsIds))
                            {
                                ProviderContact.DeleteContactById(contactId, transactionHandler);
                            }
                            // Actualiza / Registra contactos.
                            foreach (var contact in Contacts)
                            {
                                if (contact.ContactID == 0)
                                {
                                    contact.ProviderID = provider.ProviderID;
                                    contact.Insert(transactionHandler);
                                }
                                else
                                {
                                    contact.Update(transactionHandler);
                                }
                            }
                            transactionHandler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Proveedor actualizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint PV205
                    if (DbHelpers.CheckFKConstraintViolation(dbException))
                    {
                        const string warningText = "No se puede realizar la operación: el elemento tiene otras dependencias dentro del sistema."
                                                 + "\n\nEjemplo: no se puede eliminar un cliente que tiene presupuestos registrados.";
                        MessageBox.Show(warningText, "Error de integridad referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Error en servidor MySQL.\n\nMensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Logger.AppendLog("Exception at Waypoint PV205 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnManageContacts_Click(object sender, EventArgs e)
        {
            using (var form = new PV_ContactManager(Contacts))
            {
                form.ShowDialog();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnl_1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El campo [Nombre del proveedor] es obligatorio y no puede quedar en blanco o constar unicamente de espacios.",
                "Información de validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void pnl_2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El campo [CUIT/DNI] es obligatorio. Los formatos admitidos son los siguientes:"
                + Environment.NewLine + Environment.NewLine + "CUIT: XX-XXXXXXXX-XX"
                + Environment.NewLine + "DNI: XXXXXXXX (8 dígitos) o XXXXXXX (7 dígitos)"
                + Environment.NewLine + Environment.NewLine + "Ejemplos:"
                + Environment.NewLine + Environment.NewLine + "CUIT: 20-41710136-3"
                + Environment.NewLine + "DNI: 41710136", "Información de validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dtpRegistryDate_ValueChanged(object sender, EventArgs e)
        {
            double antiquity = Math.Round((DateTime.Now.Date - dtpRegistryDate.Value.Date).TotalDays);
            if (antiquity > 365)
            {
                txtAntiquity.Text = $"{Math.Round(antiquity / 365)} año(s).";
            }
            else if (antiquity > 30)
            {
                txtAntiquity.Text = $"{Math.Round(antiquity / 30)} mes(es).";
            }
            else if (antiquity > 0)
            {
                txtAntiquity.Text = $"{Math.Round(antiquity)} día(s).";
            }
            else
            {
                txtAntiquity.Text = string.Empty;
            }
        }

        private async void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDistrict.SelectedIndexChanged -= cboDistrict_SelectedIndexChanged;
            txtCity.Validating -= txtCity_Validating;
            cboDistrict.DataSource = null;
            cboDistrict.DropDownStyle = ComboBoxStyle.DropDown;
            cboDistrict.Text = string.Empty;
            txtCity.AutoCompleteCustomSource = null;
            txtCity.Text = string.Empty;
            txtAddress.Text = string.Empty;
            if ((int)(cboCountry.SelectedValue) == 9)
            {
                try
                {
                    cboDistrict.DisplayMember = "DistrictName";
                    cboDistrict.ValueMember = "DistrictID";
                    cboDistrict.DataSource = await Task.Run(() => District.GetDistrictsByCountryID(9));
                    cboDistrict.DropDownStyle = ComboBoxStyle.DropDownList;
                    var locations = await Task.Run(() => City.GetCities(9, 1));
                    var citiesAutoCompleteSource = new AutoCompleteStringCollection();
                    citiesAutoCompleteSource.AddRange(locations.Select(l => $"{l.CityName} ({l.PostalCode})").ToArray());
                    txtCity.AutoCompleteCustomSource = citiesAutoCompleteSource;
                    cboDistrict.SelectedIndexChanged += cboDistrict_SelectedIndexChanged;
                    txtCity.Validating += txtCity_Validating;
                }
                catch (Exception dbException)
                {
                    // Waypoint PV206
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint PV206 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                }
            }
        }
        private async void cboDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCity.Text = string.Empty;
            try
            {
                int selectedDistrictID = (int)cboDistrict.SelectedValue;
                var locations = await Task.Run(() => City.GetCities(9, selectedDistrictID));
                var citiesAutoCompleteSource = new AutoCompleteStringCollection();
                citiesAutoCompleteSource.AddRange(locations.Select(l => $"{l.CityName} ({l.PostalCode})").ToArray());
                txtCity.AutoCompleteCustomSource = citiesAutoCompleteSource;
            }
            catch (Exception dbException)
            {
                // Waypoint PV207
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint PV207 (Flag: MySQL). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
            }
        }
        private void txtCity_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtCity.Text != string.Empty && !txtCity.AutoCompleteCustomSource.Contains(txtCity.Text))
            {
                MessageBox.Show("La ciudad ingresada no es válida en el distrito seleccionado.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCity.Text = string.Empty;
            }
        }
    }
}
