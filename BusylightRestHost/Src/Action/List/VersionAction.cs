using Busylight;
using BusylightRestHost.Utils;

namespace BusylightRestHost.Action.List
{
    public class VersionAction : Action
    {
        private readonly string _version;

        public VersionAction(string overridenVersion = null) : base(null, null)
        {
            _version = overridenVersion ?? Version.Get();
        }

        public VersionAction(ISDK sdk, ActionParameters parameters) : base(sdk, parameters)
        {
            _version = Version.Get();
        }

        public override string Execute()
        {
            return Json.Serialize(typeof(VersionTo), new VersionTo(_version));
        }
    }
}