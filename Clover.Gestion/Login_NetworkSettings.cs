using Clover.DbLayer;
using Clover.Shared;
using System;
using System.IO;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class Login_NetworkSettings : Form
    {
        public Login_NetworkSettings()
        {
            InitializeComponent();
        }

        private void Login_NetworkSettings_Load(object sender, EventArgs e)
        {
            // Carga configuración actual.
            txtMySqlServerIP.Text = AppEnvironment.CurrentSettings.MySqlServerIP;
            txtMySqlServerPort.Text = AppEnvironment.CurrentSettings.MySqlServerPort.ToString();
            txtDatabaseName.Text = AppEnvironment.CurrentSettings.DatabaseName;
            txtChatServerIP.Text = AppEnvironment.CurrentSettings.ChatServerIP;
            txtChatServerPort.Text = AppEnvironment.CurrentSettings.ChatServerPort.ToString();
        }
        
        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Guarda posibles cambios.
            AppEnvironment.CurrentSettings.MySqlServerIP = txtMySqlServerIP.Text.Trim();
            AppEnvironment.CurrentSettings.ChatServerIP = txtChatServerIP.Text.Trim();
            DbLayerSettings.SetConnectionString(AppEnvironment.CurrentSettings.MySqlServerIP, AppEnvironment.CurrentSettings.MySqlServerPort, AppEnvironment.CurrentSettings.DatabaseName, AppEnvironment.CurrentSettings.MySqlUserName, AppEnvironment.CurrentSettings.MySqlUserPassword.DecodeBase64());
            try
            {
                AppEnvironment.CurrentSettings.SaveToFile(Path.Combine(Application.StartupPath, "settings.xml"));
            }
            catch (Exception exception)
            {
                // Waypoint LI004
                MessageBox.Show("(LI004) Error al guardar la configuración del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint LI004. Message: " + exception.Message);
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
