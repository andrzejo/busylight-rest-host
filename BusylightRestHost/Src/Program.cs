using System;
using System.Windows.Forms;
using BusylightRestHost.Utils;

namespace BusylightRestHost
{
    internal class Context : ApplicationContext
    {
        public Context()
        {
            var httpServer = new HttpServer();
            try
            {
                httpServer.Start();
            }
            catch (Exception e)
            {
                Dialogs.Error("Failed to start application. Check application is already running.");
                Logger.GetLogger().Error(e, "Failed to start HttpServer.");
                Environment.Exit(1);
            }

            new TrayMenu();
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