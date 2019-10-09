using System.Configuration;

namespace BusylightRestHost.Utils
{
    sealed class Settings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool EnableActionNotifications
        {
            get => (bool) this["EnableActionNotifications"];
            set => this["EnableActionNotifications"] = value;
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool EnableDeviceNotifications
        {
            get => (bool) this["EnableDeviceNotifications"];
            set => this["EnableDeviceNotifications"] = value;
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool EnableErrorsNotifications
        {
            get => (bool) this["EnableErrorsNotifications"];
            set => this["EnableErrorsNotifications"] = value;
        }

        public bool Exists(string property)
        {
            foreach (SettingsProperty p in Properties)
            {
                if (p.Name.Equals(property))
                {
                    return true;
                }
            }

            return false;
        }
    }
}