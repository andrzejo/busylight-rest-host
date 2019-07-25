using Busylight;

namespace BusylightRestHost.Action
{
    public class ColorAction : Action
    {
        public ColorAction(ISDK sdk, ActionParameters parameters) : base(sdk, parameters)
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