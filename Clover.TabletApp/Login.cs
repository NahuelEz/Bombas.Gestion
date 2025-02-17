using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.TabletApp
{
    public partial class Login : Form
    {
        public bool LogInAuthorized = false;
        private bool sqlConnectionBusy = false;

        public Login()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    {
                        this.SelectNextControl(this.ActiveControl, false, true, true, false);
                        return true;
                    }
                case Keys.Down:
                    {
                        this.SelectNextControl(this.ActiveControl, true, true, true, false);
                        return true;
                    }
                case Keys.Enter:
                    {
                        btnSignIn.PerformClick();
                        return true;
                    }
                case Keys.Escape:
                    {
                        btnCancel.PerformClick();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            if (sqlConnectionBusy)
            {
                MessageBox.Show("Por favor, espere mientras se completa la operación actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string inputUserName = txtUserName.Text;
            string inputPassword = txtPassword.Text;
            User potentialUser;
            try
            {
                sqlConnectionBusy = true;
                potentialUser = await Task.Run(() => User.GetPotentialUserByUserName(inputUserName));
            }
            catch (Exception dbException)
            {
                // Waypoint LI001
                MessageBox.Show("(LI001) Error de conexión. Verifique conexión a la red.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint LI001 (Flag: MySql). Message: " + dbException.Message);
                return;
            }
            finally
            {
                sqlConnectionBusy = false;
            }
            if (potentialUser == null || potentialUser.Password.DecodeBase64() != inputPassword)
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Text = string.Empty;
                return;
            }
            if (potentialUser.AccessLevel < 2)
            {
                MessageBox.Show("El usuario especificado no tiene los permisos necesarios para utilizar esta aplicación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Text = string.Empty;
                return;
            }
            AppEnvironment.CurrentUser = potentialUser;
            DbLayerSettings.SetUserID(potentialUser.UserID);
            LogInAuthorized = true;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
