using System;
using System.Threading;
using System.Windows.Forms;

namespace Clover.ChatServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "Clover.ChatServer", out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    new ServerAdmin().InitializeServer();
                    Application.Run();
                }
                else
                {
                    MessageBox.Show("Server is already running.", "Clover.ChatServer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
