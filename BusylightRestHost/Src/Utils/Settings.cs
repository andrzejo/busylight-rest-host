using System.Configuration;

namespace BusylightRestHost.Utils
{
    sealed class Settings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool EnableNotifications
        {
            get { return (bool) this["EnableNotifications"]; }
            set { this["EnableNotifications"] = value; }
        }
    }
}