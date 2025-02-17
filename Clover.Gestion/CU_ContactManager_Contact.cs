using Clover.DbLayer;
using System;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CU_ContactManager_Contact : Form
    {
        private CustomerContact CurrentContact = null;

        public CU_ContactManager_Contact(CustomerContact Contact = null)
        {
            this.CurrentContact = Contact;
            InitializeComponent();
        }

        private void CU_ContactManager_Contact_Load(object sender, EventArgs e)
        {
            if (CurrentContact != null)
            {
                txtContactName.Text = CurrentContact.ContactName;
                txtGreeting.Text = CurrentContact.Greeting;
                txtPhone.Text = CurrentContact.Phone;
                txtSecondaryPhone.Text = CurrentContact.SecondaryPhone;
                txtEmail.Text = CurrentContact.Email;
                btnAccept.Text = "Guardar";
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Validación
            if (string.IsNullOrWhiteSpace(txtContactName.Text) || string.IsNullOrWhiteSpace(txtGreeting.Text) 
                || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos para continuar.","Atención",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (CurrentContact == null)
            {
                ((CU_ContactManager)(this.Owner)).Contacts.Add(new CustomerContact()
                {
                    ContactName = txtContactName.Text,
                    Greeting = txtGreeting.Text,
                    Phone = txtPhone.Text,
                    SecondaryPhone = txtSecondaryPhone.Text,
                    Email = txtEmail.Text
                });
                this.Close();
            }
            else
            {
                CurrentContact.ContactName = txtContactName.Text;
                CurrentContact.Greeting = txtGreeting.Text;
                CurrentContact.Phone = txtPhone.Text;
                CurrentContact.SecondaryPhone = txtSecondaryPhone.Text;
                CurrentContact.Email = txtEmail.Text;
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
