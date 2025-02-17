using Clover.DbLayer;
using Clover.Shared;
using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class Login : Form
    {
        public bool LogInAuthorized = false;

        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Load(object sender, EventArgs e)
        {
            // Carga configuración del sistema.
            try
            {
                var settings = Settings.LoadFromFile(Path.Combine(Application.StartupPath, "settings.xml"));
                AppEnvironment.CurrentSettings = settings;
                DbLayerSettings.SetConnectionString(settings.MySqlServerIP, settings.MySqlServerPort, settings.DatabaseName, settings.MySqlUserName, settings.MySqlUserPassword.DecodeBase64());
            }
            catch (Exception exception)
            {
                // Waypoint LI001
                MessageBox.Show("(LI001) Error al cargar la configuración del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint LI001. Message: " + exception.Message);
                Application.Exit();
                return;
            }

            // Muestra el usuario predeterminado.
            txtUserName.Text = AppEnvironment.CurrentSettings.LastUserName;

            // Muestra IPv4 LAN.
            try
            {
                lblIPAddress.Text = await Task.Run(() => GetLocalIPv4());
            }
            catch (Exception exception)
            {
                // Waypoint LI002
                Logger.AppendLog("Exception at Waypoint LI002. Message: " + exception.Message);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnNetworkSettings_Click(object sender, EventArgs e)
        {
            using (var form = new Login_NetworkSettings())
            {
                form.ShowDialog();
            }
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            string inputUserName = txtUserName.Text;
            string inputPassword = txtPassword.Text;
            User potentialUser;
            try
            {
                potentialUser = await Task.Run(() => User.GetPotentialUserByUserName(inputUserName));
            }
            catch (Exception dbException)
            {
                // Waypoint LI003
                MessageBox.Show("(LI003) Error de conexión. Verifique conexión a la red.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint LI003 (Flag: MySql). Message: " + dbException.Message);
                return;
            }

            // Validación de credenciales
            if (potentialUser == null || potentialUser.Password.DecodeBase64() != inputPassword)
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Text = string.Empty;
                return;
            }

            // Parámetro de acceso mínimo requerido
            int minimumRequiredAccessLevel = 1; // Puedes ajustar este valor según tus necesidades

            // Validación de nivel de acceso
            if (potentialUser.AccessLevel < minimumRequiredAccessLevel)
            {
                MessageBox.Show($"El usuario especificado no tiene los permisos necesarios para utilizar esta aplicación. Se requiere un nivel de acceso de {minimumRequiredAccessLevel} o superior.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Text = string.Empty;
                return;
            }

            // Si el login es exitoso
            AppEnvironment.CurrentUser = potentialUser;
            DbLayerSettings.SetUserID(potentialUser.UserID);
            LogInAuthorized = true;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetLocalIPv4()
        {
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                var adapterProperties = item.GetIPProperties();
                if ((item.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || item.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    && item.OperationalStatus == OperationalStatus.Up && adapterProperties.GatewayAddresses.FirstOrDefault() != null)
                {
                    foreach (var ip in adapterProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}