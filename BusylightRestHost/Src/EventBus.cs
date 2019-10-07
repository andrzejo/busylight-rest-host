using System;
using System.Collections.Generic;

namespace BusylightRestHost
{
    public class EventBus
    {
        private static EventBus _instance;

        private readonly Dictionary<string, List<System.Action<string, Dictionary<string, string>>>> handlers =
            new Dictionary<string, List<Action<string, Dictionary<string, string>>>>();

        public static EventBus GetInstance()
        {
            return _instance ?? (_instance = new EventBus());
        }

        public void Bind(string eventName, System.Action<string, Dictionary<string, string>> method)
        {
            if (!handlers.ContainsKey(eventName))
            {
                handlers[eventName] = new List<Action<string, Dictionary<string, string>>>();
            }
            handlers[eventName].Add(method);
        }

        public void Trigger(string eventName, Dictionary<string, string> parameters)
        {
            if (handlers.ContainsKey(eventName))
            {
                handlers[eventName].ForEach((h) => { h.Invoke(eventName, parameters); });
            }
        }
    }
}