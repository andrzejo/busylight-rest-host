using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusylightRestHost
{
    internal class Context : ApplicationContext
    {
        public Context()
        {
            var httpServer = new HttpServer();
            httpServer.Start();
        }
    }


    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Context());
        }
    }
}