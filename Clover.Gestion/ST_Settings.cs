using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class ST_Settings : Form
    {
        private string EstimateEmailBodyA = null;
        private string ReminderEmailBodyA = null;
        private string PurchaseOrderEmailBodyA = null;
        private string EstimateEmailBodyB = null;
        private string ReminderEmailBodyB = null;
        private string PurchaseOrderEmailBodyB = null;

        public ST_Settings()
        {
            InitializeComponent();
            txtPdfDocumentsFolder.Text = @"G:\.shortcut-targets-by-id\0B5bbyJdzImtSenJaRXlCOEFmZGc\DOCUMENTOS";
        }

        private async void ST_Settings_Load(object sender, EventArgs e)
        {
            MailServer mailServerA = null;
            MailServer mailServerB = null;
            MailSetting mailSettingA = null;
            MailSetting mailSettingB = null;

            try
            {
                // Obtener configuración del servidor de correos y ajustes de correo
                mailServerA = await Task.Run(() => MailServer.GetMailServerByBusinessId(1));
                mailServerB = await Task.Run(() => MailServer.GetMailServerByBusinessId(2));

                // Intentamos obtener las configuraciones de correo para el usuario actual
                mailSettingA = await Task.Run(() => MailSetting.GetMailSettingByUserIdAndBusinessId(AppEnvironment.CurrentUser.UserID, 1));
                mailSettingB = await Task.Run(() => MailSetting.GetMailSettingByUserIdAndBusinessId(AppEnvironment.CurrentUser.UserID, 2));
            }
            catch (Exception DbException)
            {
                // Manejo de error si falla la consulta a la base de datos, pero no detenemos la aplicación
                MessageBox.Show("Error en servidor MySQL." + Environment.NewLine + "Mensaje: " + DbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Si los datos de mailSettingA o mailSettingB son nulos, simplemente no asignamos ningún valor y los campos quedarán vacíos.

            if (mailServerA != null)
            {
                txtSmtpServerA.Text = mailServerA.SmtpServer;
                nudSmtpServerPortA.Value = mailServerA.SmtpServerPort;
                chkEnableSslA.Checked = mailServerA.SmtpServerEnableSsl;
                txtEmailAddressA.Text = mailServerA.EmailAddress;
                txtEmailPasswordA.Text = mailServerA.EmailPassword;
            }

            if (mailSettingA != null)
            {
                txtCopyToEmailAddressA.Text = mailSettingA.CopyToEmailAddress;
                txtEstimateEmailSubjectA.Text = mailSettingA.EstimateEmailSubject;
                txtReminderEmailSubjectA.Text = mailSettingA.ReminderEmailSubject;
                txtPurchaseOrderEmailSubjectA.Text = mailSettingA.PurchaseOrderEmailSubject;

                // Asignar valores de cuerpo de email a las variables de la clase
                EstimateEmailBodyA = mailSettingA.EstimateEmailBody;
                ReminderEmailBodyA = mailSettingA.ReminderEmailBody;
                PurchaseOrderEmailBodyA = mailSettingA.PurchaseOrderEmailBody;
            }

            if (mailServerB != null)
            {
                txtSmtpServerB.Text = mailServerB.SmtpServer;
                nudSmtpServerPortB.Value = mailServerB.SmtpServerPort;
                chkEnableSslB.Checked = mailServerB.SmtpServerEnableSsl;
                txtEmailAddressB.Text = mailServerB.EmailAddress;
                txtEmailPasswordB.Text = mailServerB.EmailPassword;
            }

            if (mailSettingB != null)
            {
                txtCopyToEmailAddressB.Text = mailSettingB.CopyToEmailAddress;
                txtEstimateEmailSubjectB.Text = mailSettingB.EstimateEmailSubject;
                txtReminderEmailSubjectB.Text = mailSettingB.ReminderEmailSubject;
                txtPurchaseOrderEmailSubjectB.Text = mailSettingB.PurchaseOrderEmailSubject;

                // Asignar valores de cuerpo de email a las variables de la clase
                EstimateEmailBodyB = mailSettingB.EstimateEmailBody;
                ReminderEmailBodyB = mailSettingB.ReminderEmailBody;
                PurchaseOrderEmailBodyB = mailSettingB.PurchaseOrderEmailBody;
            }

            // Establecer la ruta predeterminada para guardar documentos PDF
            txtPdfDocumentsFolder.Text = @"G:\.shortcut-targets-by-id\0B5bbyJdzImtSenJaRXlCOEFmZGc\DOCUMENTOS";

            // Opcional: Asegurar que la ruta predeterminada se establece solo si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtPdfDocumentsFolder.Text))
            {
                txtPdfDocumentsFolder.Text = @"G:\.shortcut-targets-by-id\0B5bbyJdzImtSenJaRXlCOEFmZGc\DOCUMENTOS";
            }
        }





        // Pestaña: General
        private void btnChangeNotesDestinationFolder_Click(object sender, EventArgs e)
        {
            using (var fbdBrowser = new FolderBrowserDialog())
            {
                if (fbdBrowser.ShowDialog() == DialogResult.OK)
                {
                    txtPdfDocumentsFolder.Text = fbdBrowser.SelectedPath;
                }
            }
        }
        // Pestaña: Envío de correos
        private void btnEditEstimateEmailBodyA_Click(object sender, EventArgs e)
        {
            using (var form = new ST_HtmlEditor(EstimateEmailBodyA))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    EstimateEmailBodyA = form.HtmlContent;
                }
            }
        }
        private void btnEditReminderEmailBodyA_Click(object sender, EventArgs e)
        {
            using (var form = new ST_HtmlEditor(ReminderEmailBodyA))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ReminderEmailBodyA = form.HtmlContent;
                }
            }
        }
        private void btnEditPurchaseOrderEmailBodyA_Click(object sender, EventArgs e)
        {
            using (var form = new ST_HtmlEditor(PurchaseOrderEmailBodyA))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    PurchaseOrderEmailBodyA = form.HtmlContent;
                }
            }
        }
        private void btnEditEstimateEmailBodyB_Click(object sender, EventArgs e)
        {
            using (var form = new ST_HtmlEditor(EstimateEmailBodyB))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    EstimateEmailBodyB = form.HtmlContent;
                }
            }
        }
        private void btnEditReminderEmailBodyB_Click(object sender, EventArgs e)
        {
            using (var form = new ST_HtmlEditor(ReminderEmailBodyB))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ReminderEmailBodyB = form.HtmlContent;
                }
            }
        }
        private void btnEditPurchaseOrderEmailBodyB_Click(object sender, EventArgs e)
        {
            using (var form = new ST_HtmlEditor(PurchaseOrderEmailBodyB))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    PurchaseOrderEmailBodyB = form.HtmlContent;
                }
            }
        }
        private void chkShowPasswordA_CheckedChanged(object sender, EventArgs e)
        {
            txtEmailPasswordA.UseSystemPasswordChar = !chkShowPasswordA.Checked;
        }
        private void chkShowPasswordB_CheckedChanged(object sender, EventArgs e)
        {
            txtEmailPasswordB.UseSystemPasswordChar = !chkShowPasswordB.Checked;
        }
        // Pestaña: Respaldo información
        private void btnOpenBackupDumpTool_Click(object sender, EventArgs e)
        {
            using (var ofdDumpToolOpen = new OpenFileDialog() { Filter = "Archivos ejecutables (.exe)|*.exe", FileName = "mysqldump.exe" })
            {
                if (ofdDumpToolOpen.ShowDialog() == DialogResult.OK)
                {
                    txtBackupDumpTool.Text = ofdDumpToolOpen.FileName;
                }
            }
        }
        private void btnOpenBackupFolder_Click(object sender, EventArgs e)
        {
            using (var fbdBackupFolderOpen = new FolderBrowserDialog())
            {
                if (fbdBackupFolderOpen.ShowDialog() == DialogResult.OK)
                {
                    txtBackupFolder.Text = fbdBackupFolderOpen.SelectedPath;
                }
            }
        }
        private async void btnMakeBackup_Click(object sender, EventArgs e)
        {
            string arguments = $"--host=\"{AppEnvironment.CurrentSettings.MySqlServerIP}\" --port=\"{AppEnvironment.CurrentSettings.MySqlServerPort}\""
                                 + $" --user=\"{AppEnvironment.CurrentSettings.MySqlUserName}\" --password=\"{AppEnvironment.CurrentSettings.MySqlUserPassword.DecodeBase64()}\""
                                 + $" --default-character-set=utf8 --databases \"{AppEnvironment.CurrentSettings.DatabaseName}\"";
            var backupTime = DateTime.Today;
            string filename = Path.Combine(AppEnvironment.CurrentSettings.BackupFolder, $"backup{backupTime:ddMMyy}.sql");
            int processExitCode;
            try
            {
                if (!Directory.Exists(AppEnvironment.CurrentSettings.BackupFolder))
                {
                    Directory.CreateDirectory(AppEnvironment.CurrentSettings.BackupFolder);
                }
                using (var dumpProcess = new Process())
                {
                    dumpProcess.StartInfo.FileName = AppEnvironment.CurrentSettings.BackupDumpTool;
                    dumpProcess.StartInfo.Arguments = arguments;
                    dumpProcess.StartInfo.UseShellExecute = false;
                    dumpProcess.StartInfo.RedirectStandardOutput = true;
                    dumpProcess.StartInfo.CreateNoWindow = true;
                    dumpProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    dumpProcess.Start();
                    using (var outputStream = dumpProcess.StandardOutput.BaseStream)
                    {
                        using (var fileStream = File.Create(filename))
                        {
                            await outputStream.CopyToAsync(fileStream);
                        }
                    }
                    dumpProcess.WaitForExit();
                    processExitCode = dumpProcess.ExitCode;
                }
            }
            catch (Exception backupException)
            {
                // Waypoint ST002
                MessageBox.Show("Error al realizar respaldo de base de datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ST002. Message: " + backupException.Message);
                return;
            }
            if (processExitCode == 0)
            {
                MessageBox.Show("Respaldo realizado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.AppendLog("Manual backup successfully performed.");
                try
                {
                    // Overwrites lastBackup.sql
                    File.Copy(filename, Path.Combine(AppEnvironment.CurrentSettings.BackupFolder, "lastBackup.sql"), true);
                    // Updates config
                    AppEnvironment.CurrentSettings.LastBackup = backupTime.ToString("dd/MM/yyyy");
                    AppEnvironment.CurrentSettings.SaveToFile(Path.Combine(Application.StartupPath, "settings.xml"));
                }
                catch (Exception exception)
                {
                    // Waypoint ST003
                    MessageBox.Show("Error en tareas asociadas a respaldo de base de datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint ST003. Message: " + exception.Message);
                    return;
                }
                lblLastBackup.Text = backupTime.ToString("dd/MM/yyyy");
            }
            else
            {
                // Waypoint ST004
                MessageBox.Show("Error al realizar respaldo de base de datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ST004. Message: Unexpected process exit code " + processExitCode);
            }
        }
        // Pestaña: Usuario
        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtRepeatNewPassword.Text)
            {
                MessageBox.Show("Las nuevas contraseñas ingresadas no coinciden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtCurrentPassword.Text != AppEnvironment.CurrentUser.Password.DecodeBase64())
            {
                MessageBox.Show("Contraseña actual incorrecta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string cipherPassword = txtNewPassword.Text.EncodeBase64();
            try
            {
                await Task.Run(() => User.ChangePassword(AppEnvironment.CurrentUser.UserID, cipherPassword));
                AppEnvironment.CurrentUser.Password = cipherPassword;
                MessageBox.Show("Contraseña modificada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception dbException)
            {
                // Waypoint ST005
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ST005 (Flag: MySQL). Message: " + dbException.Message);
            }
            finally
            {
                txtCurrentPassword.Text = string.Empty;
                txtNewPassword.Text = string.Empty;
                txtRepeatNewPassword.Text = string.Empty;
            }
        }
        private void chkShowNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = !chkShowPasswords.Checked;
            txtNewPassword.UseSystemPasswordChar = !chkShowPasswords.Checked;
            txtRepeatNewPassword.UseSystemPasswordChar = !chkShowPasswords.Checked;
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {
            // Actualiza archivo de configuración XML.
            try
            {
                AppEnvironment.CurrentSettings.PdfDocumentsFolder = txtPdfDocumentsFolder.Text;
                if (rbnIntegralItems.Checked)
                {
                    AppEnvironment.CurrentSettings.PdfKeepItemTogether = "Yes";
                }
                else if (rbnFillPage.Checked)
                {
                    AppEnvironment.CurrentSettings.PdfKeepItemTogether = "No";
                }
                else
                {
                    AppEnvironment.CurrentSettings.PdfKeepItemTogether = "Ask";
                }
                if (AppEnvironment.CurrentSettings.BackupEnabled)
                {
                    AppEnvironment.CurrentSettings.BackupFrecuency = (int)nudBackupFrecuency.Value;
                    AppEnvironment.CurrentSettings.BackupDumpTool = txtBackupDumpTool.Text;
                    AppEnvironment.CurrentSettings.BackupFolder = txtBackupFolder.Text;
                }
                AppEnvironment.CurrentSettings.SaveToFile(Path.Combine(Application.StartupPath, "settings.xml"));
            }
            catch (Exception exceptionDuringXmlWrite)
            {
                // Waypoint ST006
                MessageBox.Show("Error al modificar la configuración del sistema."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exceptionDuringXmlWrite.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ST006. Message: " + exceptionDuringXmlWrite.Message);
                return;
            }
            // Actualiza elementos en servidor MySql.
            var mailServerA = new MailServer()
            {
                BusinessID = 1,
                SmtpServer = txtSmtpServerA.Text,
                SmtpServerPort = (int)nudSmtpServerPortA.Value,
                SmtpServerEnableSsl = chkEnableSslA.Checked,
                EmailAddress = txtEmailAddressA.Text,
                EmailPassword = txtEmailPasswordA.Text
            };
            var mailServerB = new MailServer()
            {
                BusinessID = 2,
                SmtpServer = txtSmtpServerB.Text,
                SmtpServerPort = (int)nudSmtpServerPortB.Value,
                SmtpServerEnableSsl = chkEnableSslB.Checked,
                EmailAddress = txtEmailAddressB.Text,
                EmailPassword = txtEmailPasswordB.Text
            };
            var mailSettingA = new MailSetting()
            {
                UserID = AppEnvironment.CurrentUser.UserID,
                BusinessID = 1,
                CopyToEmailAddress = txtCopyToEmailAddressA.Text,
                EstimateEmailSubject = txtEstimateEmailSubjectA.Text,
                EstimateEmailBody = EstimateEmailBodyA,
                ReminderEmailSubject = txtReminderEmailSubjectA.Text,
                ReminderEmailBody = ReminderEmailBodyA,
                PurchaseOrderEmailSubject = txtPurchaseOrderEmailSubjectA.Text,
                PurchaseOrderEmailBody = PurchaseOrderEmailBodyA
            };
            var mailSettingB = new MailSetting()
            {
                UserID = AppEnvironment.CurrentUser.UserID,
                BusinessID = 2,
                CopyToEmailAddress = txtCopyToEmailAddressB.Text,
                EstimateEmailSubject = txtEstimateEmailSubjectB.Text,
                EstimateEmailBody = EstimateEmailBodyB,
                ReminderEmailSubject = txtReminderEmailSubjectB.Text,
                ReminderEmailBody = ReminderEmailBodyB,
                PurchaseOrderEmailSubject = txtPurchaseOrderEmailSubjectB.Text,
                PurchaseOrderEmailBody = PurchaseOrderEmailBodyB
            };
            string notifications = string.Empty;
            if (clbEvents.CheckedItems.Count > 0)
            {
                notifications = string.Join("-", clbEvents.CheckedItems.Cast<Event>().Select(x => x.EventCode));
            }
            AppEnvironment.CurrentUser.Notifications = notifications;
            try
            {
                await Task.Run(() =>
                {
                    using (var handler = new DbTransactionHandler())
                    {
                        mailServerA.Update(handler);
                        mailServerB.Update(handler);
                        mailSettingA.Update(handler);
                        mailSettingB.Update(handler);
                        User.UpdateNotifications(AppEnvironment.CurrentUser.UserID, notifications, handler);
                        handler.CommitTransaction();
                    }
                });
            }
            catch (Exception dbException)
            {
                // Waypoint ST007
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint ST007 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}