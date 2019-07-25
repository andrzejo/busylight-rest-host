using System;
using System.Collections.Generic;
using Busylight;

namespace BusylightRestHost.Action
{
    public class Factory
    {
        private readonly ISDK _sdk;
        private readonly Dictionary<string, Type> _actions = new Dictionary<string, Type>();

        public Factory(ISDK sdk)
        {
            _sdk = sdk;
            _actions.Add("color", typeof(ColorAction));
            _actions.Add("version", typeof(VersionAction));
        }

        public IAction Create(ActionData data)
        {
            var type = _actions.TryGetValue(data.GetAction(), out var value) ? value : null;
            if (type == null)
            {
                throw new ArgumentException($"Invalid action '{data.GetAction()}'.");
            }

            return (IAction) Activator.CreateInstance(type, _sdk, data.GetParameters());
        }
    }
}