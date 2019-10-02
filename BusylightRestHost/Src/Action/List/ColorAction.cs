using Busylight;

namespace BusylightRestHost.Action.List
{
    public class ColorAction : Action
    {
        public ColorAction(SDK sdk, ActionParameters parameters) : base(sdk, parameters)
        {
        }

        public override string Execute()
        {
            var color = _parameters.GetColor();
            _sdk.Light(color);
            return "";
        }
    }
}