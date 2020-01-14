using System;
using Busylight;
using BusylightRestHost.Sdk;

namespace BusylightRestHost.Action.List
{
    public class BlinkAction : Action
    {
        public BlinkAction(ISdk sdk, ActionParameters parameters) : base(sdk, parameters)
        {
        }

        public override string Execute()
        {
            var color = _parameters.GetColor();
            var onTime = _parameters.GetInt("onTime");
            var offTime = _parameters.GetInt("offTime");
            var onSec = Math.Round((double) onTime / 10, 2);
            var offSec = Math.Round((double) offTime / 10, 2);
            Events.GetInstance()
                .TriggerAction(
                    $"Blink {_parameters.GetColorName().ToLower()} color - on/off time: {onSec}s/{offSec}s'.");
            _sdk.Blink(color, onTime, offTime);
            return "";
        }
    }
}