using Clover.Shared;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Clover.BackupTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.SetOutputFilename(Path.Combine(Application.StartupPath, "Clover.BackupTask.log"));
            Settings settings;
            try
            {
                settings = Settings.LoadFromFile(Path.Combine(Application.StartupPath, "settings.xml"));
            }
            catch (Exception exception)
            {
                // Waypoint BT001
                MessageBox.Show("Error al leer el archivo de configuración.", "Respaldo automático Clover Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint BT001. Message: " + exception.Message);
                return;
            }
            DateTime lastBackup;
            bool lastBackupParsed = DateTime.TryParseExact(settings.LastBackup, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out lastBackup);
            if (!lastBackupParsed || (DateTime.Today >= (lastBackup.AddDays(settings.BackupFrecuency.Value)).Date))
            {
                string arguments = $"--host=\"{settings.MySqlServerIP}\" --port=\"{settings.MySqlServerPort}\""
                                 + $" --user=\"{settings.MySqlUserName}\" --password=\"{settings.MySqlUserPassword.DecodeBase64()}\""
                                 + $" --default-character-set=utf8 --databases \"{settings.DatabaseName}\"";
                var backupTime = DateTime.Today;
                string filename = Path.Combine(settings.BackupFolder, $"backup{backupTime:ddMMyy}.sql");
                int processExitCode;
                try
                {
                    if (!Directory.Exists(settings.BackupFolder))
                    {
                        Directory.CreateDirectory(settings.BackupFolder);
                    }
                    using (var dumpProcess = new Process())
                    {
                        dumpProcess.StartInfo.FileName = settings.BackupDumpTool;
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
                                outputStream.CopyTo(fileStream);
                            }
                        }
                        dumpProcess.WaitForExit();
                        processExitCode = dumpProcess.ExitCode;
                    }
                }
                catch (Exception backupException)
                {
                    // Waypoint BT002
                    MessageBox.Show("Error al realizar respaldo de base de datos.", "Respaldo automático Clover Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint BT002. Message: " + backupException.Message);
                    return;
                }
                if (processExitCode == 0)
                {
                    MessageBox.Show("Respaldo programado realizado con éxito.", "Respaldo automático Clover Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.AppendLog("Scheduled backup successfully performed.");
                    try
                    {
                        // Overwrites lastBackup.sql
                        File.Copy(filename, Path.Combine(settings.BackupFolder, "lastBackup.sql"), true);
                        // Updates config
                        settings.LastBackup = backupTime.ToString("dd/MM/yyyy");
                        settings.SaveToFile(Path.Combine(Application.StartupPath, "settings.xml"));
                    }
                    catch (Exception exception)
                    {
                        // Waypoint BT003
                        MessageBox.Show("Error en tareas asociadas a respaldo de base de datos.", "Respaldo automático Clover Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.AppendLog("Exception at Waypoint BT003. Message: " + exception.Message);
                        return;
                    }
                }
                else
                {
                    // Waypoint BT004
                    MessageBox.Show("Error al realizar respaldo de base de datos.", "Respaldo automático Clover Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.AppendLog("Exception at Waypoint BT004. Message: Unexpected process exit code " + processExitCode);
                }
            }
            else
            {
                Logger.AppendLog("Current backup is up to date.");
            }
        }
    }
}
