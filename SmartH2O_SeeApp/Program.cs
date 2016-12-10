using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartH2O_SeeApp
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
            
            Thread thread1 = new Thread((ThreadStart)delegate { Application.Run(new Form1()); });
            thread1.Start();
            Application.Run(new Form2());
        }
    }
}
