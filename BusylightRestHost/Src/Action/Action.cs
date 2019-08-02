using Busylight;

namespace BusylightRestHost.Action
{
    public abstract class Action : IAction
    {
        protected readonly ISDK _sdk;
        protected readonly ActionParameters _parameters;

        protected Action(ISDK sdk, ActionParameters parameters)
        {
            _sdk = sdk;
            _parameters = parameters;
        }

        public abstract string Execute();
    }
}