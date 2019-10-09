using System.Collections.Generic;

namespace BusylightRestHost
{
    public class Events
    {
        public static string ACTION_EVENT = "action_event";
        public static string ERROR_EVENT = "error_event";
        public static string DEVICE_CHANGE_EVENT = "device_change_event";

        private static Events _instance;

        public static Events GetInstance()
        {
            return _instance ?? (_instance = new Events());
        }

        public void TriggerAction(string text)
        {
            TriggerEvent(ACTION_EVENT, text);
        }

        public void TriggerShowError(string text)
        {
            TriggerEvent(ERROR_EVENT, text);
        }

        public void TriggerDeviceChanged(string text)
        {
            TriggerEvent(DEVICE_CHANGE_EVENT, text);
        }

        private void TriggerEvent(string eventName, string text)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"text", text}
            };
            EventBus.GetInstance().Trigger(eventName, parameters);
        }
    }
}