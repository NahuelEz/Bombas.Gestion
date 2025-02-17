using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Clover.Shared
{
    public class Settings
    {
        public string MySqlServerIP { get; set; }
        public uint MySqlServerPort { get; set; }
        public string DatabaseName { get; set; }
        public string MySqlUserName { get; set; }
        public string MySqlUserPassword { get; set; }
        public string ChatServerIP { get; set; }
        public int ChatServerPort { get; set; }
        public string PdfDocumentsFolder { get; set; }
        public string PdfKeepItemTogether { get; set; }
        public string LastUserName { get; set; }
        public int StartTabIndex { get; set; }
        public int StartSubtabIndex { get; set; }
        public bool NotifyOnRepairOrderAdded { get; set; }
        public bool BackupEnabled { get; set; }
        public int? BackupFrecuency { get; set; }
        public string BackupDumpTool { get; set; }
        public string BackupFolder { get; set; }
        public string LastBackup { get; set; }
        
        public void SaveToFile(string filename)
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            using (var writer = new StreamWriter(filename, false, Encoding.UTF8))
            {
                xmlSerializer.Serialize(writer, this);
                writer.Flush();
            }
        }
        public static Settings LoadFromFile(string filename)
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            using (var reader = new StreamReader(filename, Encoding.UTF8))
            {
                return (Settings)xmlSerializer.Deserialize(reader);
            }
        }

        private Settings() { }
    }
}
