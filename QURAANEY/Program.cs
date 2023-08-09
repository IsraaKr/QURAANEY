using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using QURAANEY.SETTING;
using QURAANEY.SOURA;
//using QURAANEY.START;

namespace QURAANEY
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

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ar-AR");
                Application.Run(new F_MAIN());
        }
    }
}
