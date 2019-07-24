using Busylight;

namespace BusylightRestHost.Action
{
    public class VersionAction : Action
    {
        public VersionAction(ISDK sdk, ActionParameters parameters) : base(sdk, parameters)
        {
        }

        public override string Execute()
        {
            return Serialize(typeof(VersionAction), this);
        }
    }
}