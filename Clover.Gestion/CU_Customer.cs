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
    public partial class CU_Customer : Form
    {
        private int? CustomerID = null;
        private Customer CurrentCustomer = null;
        private bool SafeExit = false;
        private List<CustomerContact> Contacts = new List<CustomerContact>();
        
        public CU_Customer(int? CustomerID = null)
        {
            this.CustomerID = CustomerID;
            InitializeComponent();
        }
        
        private async void CU_Customer_Load(object sender, EventArgs e)
        {
            // Ajusta formulario en pantalla.
            int availableHeight = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Height > availableHeight)
            {
                this.Height = availableHeight;
                this.CenterToScreen();
            }
            // Rellena campos desplegables.
            try
            {
                cboTaxGroup.DataSource = new string[] { "Responsable Inscripto", "Monotributista", "Consumidor final", "IVA excento" };
                cboPayment.DisplayMember = "PaymentName";
                cboPayment.ValueMember = "PaymentID";
                cboPayment.DataSource = await Task.Run(() => Payment.GetPayments());
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
                // Waypoint CU201
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU201 (Flag: MySQL). Message: " + dbException.Message);
                SafeExit = true;
                this.Close();
            }
            if (CustomerID.HasValue)
            {
                try
                {
                    CurrentCustomer = await Task.Run(() => Customer.GetCustomerById(CustomerID.Value));
                    Contacts = await Task.Run(() => CustomerContact.GetContactsByCustomerId(CustomerID.Value));
                }
                catch (Exception dbException)
                {
                    // Waypoint CU202
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint CU202 (Flag: MySQL). Message: " + dbException.Message);
                    SafeExit = true;
                    this.Close();
                }
                this.Text = "Visualizando cliente: " + CurrentCustomer.CustomerName;
                txtCustomerID.Text = CurrentCustomer.CustomerID.ToString("D8");
                txtCustomerName.Text = CurrentCustomer.CustomerName;
                rbnIsCUIT.Checked = CurrentCustomer.IsCUIT; rbnIsDNI.Checked = !CurrentCustomer.IsCUIT;
                txtIdentityNumber.Text = CurrentCustomer.IdentityNumber;
                cboTaxGroup.SelectedItem = CurrentCustomer.TaxGroup;
                cboPayment.SelectedValue = CurrentCustomer.PaymentID;
                nudPaymentTerm.Value = CurrentCustomer.PaymentTerm;
                cboBusiness.SelectedValue = CurrentCustomer.BusinessID;
                dtpRegistryDate.Value = CurrentCustomer.RegistryDate;
                if (!string.IsNullOrWhiteSpace(CurrentCustomer.Address))
                {
                    if (CurrentCustomer.CountryID == 9) // 9 = Argentina
                    {
                        try
                        {
                            cboDistrict.DisplayMember = "DistrictName";
                            cboDistrict.ValueMember = "DistrictID";
                            cboDistrict.DataSource = await Task.Run(() => District.GetDistrictsByCountryID(9));
                            var locations = await Task.Run(() => City.GetCities(9, CurrentCustomer.DistrictID.Value));
                            var citiesAutoCompleteSource = new AutoCompleteStringCollection();
                            citiesAutoCompleteSource.AddRange(locations.Select(l => $"{l.CityName} ({l.PostalCode})").ToArray());
                            txtCity.AutoCompleteCustomSource = citiesAutoCompleteSource;
                        }
                        catch (Exception dbException)
                        {
                            // Waypoint CU203
                            MessageBox.Show("Error en servidor MySQL."
                                + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.AppendLog("Exception at Waypoint CU203 (Flag: MySQL). Message: " + dbException.Message);
                            SafeExit = true;
                            this.Close();
                        }
                        cboCountry.SelectedValue = CurrentCustomer.CountryID;
                        cboDistrict.SelectedValue = CurrentCustomer.DistrictID;
                        cboDistrict.DropDownStyle = ComboBoxStyle.DropDownList;
                        txtCity.Text = CurrentCustomer.City;
                        txtAddress.Text = CurrentCustomer.Address;
                        // Activa funciones evento-controladas.
                        cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
                        cboDistrict.SelectedIndexChanged += cboDistrict_SelectedIndexChanged;
                        txtCity.Validating += txtCity_Validating;
                    }
                    else
                    {
                        cboCountry.SelectedValue = CurrentCustomer.CountryID;
                        cboDistrict.Text = CurrentCustomer.District;
                        txtCity.Text = CurrentCustomer.City;
                        txtAddress.Text = CurrentCustomer.Address;
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
                cboPayment.SelectedValue = 5; // Efectivo.
                // Activa funciones evento-controladas.
                cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
            }
        }
        private void CU_Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SafeExit && CurrentCustomer == null)
            {
                string messageText = "Los cambios no guardados serán descartados.\n\n¿Desea continuar?";
                var dialog = MessageBox.Show(messageText, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = !(dialog == DialogResult.Yes);
            }
        }
        
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            bool invalidName = string.IsNullOrWhiteSpace(txtCustomerName.Text);
            bool invalidId = !Regex.IsMatch(txtIdentityNumber.Text, rbnIsCUIT.Checked ? @"^\d{2}\-\d{8}\-\d$" : @"^\d{7,8}$");
            // Muestra señal en campos inválidos.
            pnl_1.Visible = invalidName;
            pnl_2.Visible = invalidId;
            // Valida campos.
            if (invalidName || invalidId)
            {
                MessageBox.Show("Uno o más campos no poseen el formato correcto o contienen caracteres no permitidos."
                    + Environment.NewLine + Environment.NewLine + "Revise la información ingresada.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool incompleteAddress = (string.IsNullOrWhiteSpace(cboCountry.Text) || string.IsNullOrWhiteSpace(cboDistrict.Text)
                                   || string.IsNullOrWhiteSpace(txtCity.Text) || string.IsNullOrWhiteSpace(txtAddress.Text));
            // Genera objeto "customer".
            var customer = new Customer
            {
                CustomerID = (CurrentCustomer == null) ? 0 : CurrentCustomer.CustomerID,
                CustomerName = txtCustomerName.Text,
                IdentityNumber = txtIdentityNumber.Text,
                IsCUIT = rbnIsCUIT.Checked,
                TaxGroup = cboTaxGroup.Text,
                PaymentID = (int)cboPayment.SelectedValue,
                PaymentTerm = (int)nudPaymentTerm.Value,
                BusinessID = (int)cboBusiness.SelectedValue
            };
            if (!incompleteAddress)
            {
                customer.CountryID = (int)cboCountry.SelectedValue;
                customer.Country = cboCountry.Text;
                customer.DistrictID = (int)cboDistrict.SelectedValue;
                customer.District = cboDistrict.Text;
                customer.City = txtCity.Text;
                customer.Address = txtAddress.Text;
            }
            else
            {
                var dialog = MessageBox.Show("La dirección no se guardará porque está incompleta. ¿Desea continuar?", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog != DialogResult.OK)
                {
                    return;
                }
            }
            customer.RegistryDate = dtpRegistryDate.Value.Date;
            if (CurrentCustomer == null)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        using (var transactionHandler = new DbTransactionHandler())
                        {
                            // Registra nuevo cliente.
                            customer.Insert(transactionHandler);
                            // Registra contactos asociados.
                            foreach (var contact in Contacts)
                            {
                                contact.CustomerID = customer.CustomerID;
                                contact.Insert(transactionHandler);
                            }
                            transactionHandler.CommitTransaction();
                        }
                    });
                    MessageBox.Show("Cliente registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint CU204
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint CU204 (Flag: MySQL). Message: " + dbException.Message);
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
                            // Actualiza cliente existente.
                            customer.Update(transactionHandler);
                            // Remueve contactos eliminados.
                            var oldContactsIds = CustomerContact.GetContactsByCustomerId(customer.CustomerID, transactionHandler).Select(x => x.ContactID);
                            var newContactsIds = Contacts.Select(x => x.ContactID);
                            foreach (int contactId in oldContactsIds.Except(newContactsIds))
                            {
                                CustomerContact.DeleteContactById(contactId, transactionHandler);
                            }
                            // Actualiza / Registra contactos.
                            foreach (var contact in Contacts)
                            {
                                if (contact.ContactID == 0)
                                {
                                    contact.CustomerID = customer.CustomerID;
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
                    MessageBox.Show("Cliente actualizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SafeExit = true;
                    this.Close();
                }
                catch (Exception dbException)
                {
                    // Waypoint CU205
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
                    Logger.AppendLog("Exception at Waypoint CU205 (Flag: MySQL). Message: " + dbException.Message);
                }
            }
        }
        private void btnManageContacts_Click(object sender, EventArgs e)
        {
            using (var form = new CU_ContactManager(Contacts))
            {
                form.ShowDialog();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SafeExit = true;
            this.Close();
        }

        private void pnl_1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El campo [Nombre del cliente] es obligatorio y no puede quedar en blanco o constar únicamente de espacios.",
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
                    // Waypoint CU206
                    MessageBox.Show("Error en servidor MySQL."
                        + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint CU206 (Flag: MySQL). Message: " + dbException.Message);
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
                // Waypoint CU207
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CU207 (Flag: MySQL). Message: " + dbException.Message);
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
