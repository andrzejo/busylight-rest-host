using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BusylightRestHost.Utils
{
    public class Autostart
    {
        public void Toggle()
        {
            SetStartup(!IsEnabled());
        }

        public bool IsEnabled()
        {
            try
            {
                var registryKey = GetRegistryKey();
                var value = registryKey.GetValue(ApplicationText.GetAutostartText());
                return value != null;
            }
            catch (Exception e)
            {
                Logger.GetLogger().Error(e, "Failed to access registry.");
                return false;
            }
        }

        private void SetStartup(bool enabled)
        {
            try
            {
                var registryKey = GetRegistryKey();

                if (enabled)
                {
                    Logger.GetLogger().Debug("Enable autostart.");
                    registryKey.SetValue(ApplicationText.GetAutostartText(), Application.ExecutablePath);
                }
                else
                {
                    Logger.GetLogger().Debug("Disable autostart.");
                    registryKey.DeleteValue(ApplicationText.GetAutostartText(), false);
                }
            }
            catch (Exception e)
            {
                Logger.GetLogger().Error(e, "Failed to setup autostart.");
                Dialogs.Error("Failed to setup autostart.");
            }
        }

        private static RegistryKey GetRegistryKey()
        {
            var registryKey = Registry
                .CurrentUser
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            return registryKey;
        }
    }
}