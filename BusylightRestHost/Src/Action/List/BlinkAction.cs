using Busylight;

namespace BusylightRestHost.Action.List
{
    public class BlinkAction : Action
    {
        public BlinkAction(SDK sdk, ActionParameters parameters) : base(sdk, parameters)
        {
        }

        public override string Execute()
        {
            var color = _parameters.GetColor();
            var onTime = _parameters.GetInt("onTime");
            var offTime = _parameters.GetInt("offTime");
            _sdk.Blink(color, onTime, offTime);
            return "";
        }
    }
}