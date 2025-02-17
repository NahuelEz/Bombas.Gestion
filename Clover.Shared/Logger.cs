using System;
using System.IO;

namespace Clover.Shared
{
    public static class Logger
    {
        private static object lockObject = new object();
        private static string logFilename;

        public static void SetOutputFilename(string path)
        {
            logFilename = path;
        }
        public static bool AppendLog(string message)
        {
            lock (lockObject)
            {
                try
                {
                    using (var writer = new StreamWriter(logFilename, true))
                    {
                        writer.WriteLine($"{DateTime.Now:dd/MM/yy HH:mm:ss} : {message}");
                        writer.Close();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
