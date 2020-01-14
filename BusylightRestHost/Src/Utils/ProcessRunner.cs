using System.Diagnostics;

namespace BusylightRestHost.Utils
{
    public static class ProcessRunner
    {
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (xdgAvailable())
                {
                    Process.Start("xdg-open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private static bool xdgAvailable()
        {
            try
            {
                Process.Start("xdg-open", "--version");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}