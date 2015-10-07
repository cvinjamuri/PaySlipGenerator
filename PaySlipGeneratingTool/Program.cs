using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySlipGeneratingTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login logon = new Login();
            Application.Run(logon);
            if (logon.LogonSuccessful)
            {
                Application.Run(new PaySlip());
            }
           
        }
    }
}
