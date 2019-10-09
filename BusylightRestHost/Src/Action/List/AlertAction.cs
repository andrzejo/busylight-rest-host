using System.Drawing;
using Busylight;

namespace BusylightRestHost.Action.List
{
    public class AlertAction : Action
    {
        public AlertAction(SDK sdk, ActionParameters parameters) : base(sdk, parameters)
        {
        }

        public override string Execute()
        {
            var color = _parameters.GetColor();
            var sound = _parameters.GetSound();
            var volume = _parameters.GetVolume();
            Events.GetInstance().TriggerAction($"Alert with color {_parameters.GetColorName().ToLower()} and sound '{sound}', sound volume: {volume}.");
            _sdk.Alert(color, sound, volume);
            return "";
        }
    }
}