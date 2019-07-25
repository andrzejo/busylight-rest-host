using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Busylight;

namespace BusylightRestHost.Action
{
    public class Factory
    {
        private readonly IBusylightLibProvider _libProvider;

        public Factory(IBusylightLibProvider libProvider)
        {
            _libProvider = libProvider;
        }

        public IAction Create(ActionData data)
        {
            return CreateAction(data);
        }

        private IAction CreateAction(ActionData data)
        {
            var action = data.GetAction();
            var className = action.First().ToString().ToUpper() + action.Substring(1) + "Action";
            var qualifiedClassName = $"{typeof(IAction).Namespace}.List.{className}";
            var classType = Type.GetType(qualifiedClassName);
            if (classType == null)
            {
                var message = $"Action '{action}' is not implemented (Missing class '{qualifiedClassName}'";
                throw new ArgumentException(message);
            }

            return (IAction) Activator.CreateInstance(classType, _libProvider.Instance(), data.GetParameters());
        }
    }
}