using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace uWatchtable
{
    static class Program
    {
        /// <summary>
        /// General Application starting point
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CheckRun();
            // Added just in case of lack priviliges to end with noroot process
            if (Program.IsAdministrator())
                Application.Run(new StartForm());
            else
                Application.Exit();
        }

        /* Check Identity of current process with Administrator priviliges.
         * Return true := Application process is running by admin */
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /* Check if process is running as armin
         * but if not -> start new with runas and terminate old one */
        public static void CheckRun()
        {
            if (!Program.IsAdministrator())
            {
                string AppFilePath = Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(AppFilePath);
                startInfo.Verb = "runas";
                startInfo.Arguments = "restart";
                Process.Start(startInfo);
                Application.Exit();
            }
        }
    }
}
