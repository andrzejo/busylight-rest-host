using System.Collections.Generic;

namespace BusylightRestHost
{
    public class Events
    {
        public static string SHOW_TIP_EVENT = "show_tip";
        private static Events _instance;

        public static Events GetInstance()
        {
            return _instance ?? (_instance = new Events());
        }
        
        public void TriggerShowTip(string text, string type = "info")
        {
            var parameters = new Dictionary<string, string>()
            {
                {"text", text},
                {"type", type}
            };
            EventBus.GetInstance().Trigger(SHOW_TIP_EVENT, parameters);
        }
    }
}