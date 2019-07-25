using System;
using System.Collections.Generic;
using Busylight;

namespace BusylightRestHost.Action
{
    public class Factory
    {
        private readonly IBusylightLibProvider _libProvider;
        private readonly Dictionary<string, Type> _actions = new Dictionary<string, Type>();

        public Factory(IBusylightLibProvider libProvider)
        {
            _libProvider = libProvider;
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

            return (IAction) Activator.CreateInstance(type, _libProvider.Instance(), data.GetParameters());
        }
    }
}