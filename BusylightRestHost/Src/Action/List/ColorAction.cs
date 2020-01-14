using Busylight;
using BusylightRestHost.Sdk;

namespace BusylightRestHost.Action.List
{
    public class ColorAction : Action
    {
        public ColorAction(ISdk sdk, ActionParameters parameters) : base(sdk, parameters)
        {
        }

        public override string Execute()
        {
            var color = _parameters.GetColor();
            var message = IsOffColor(color) ? "Disable Busylight" : $"Set {_parameters.GetColorName().ToLower()} color.";
            Events.GetInstance().TriggerAction(message);
            _sdk.Light(color);
            return "";
        }

        private bool IsOffColor(BusylightColor color)
        {
            return color.BlueRgbValue == 0 && color.GreenRgbValue == 0 && color.RedRgbValue == 0;
        }
    }
}