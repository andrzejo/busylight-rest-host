using Busylight;
using BusylightRestHost.Sdk;

namespace BusylightRestHost.Action
{
    public abstract class Action : IAction
    {
        protected readonly ISdk _sdk;
        protected readonly ActionParameters _parameters;

        protected Action(ISdk sdk, ActionParameters parameters)
        {
            _sdk = sdk;
            _parameters = parameters;
            ValidateDeviceExists();
        }

        private void ValidateDeviceExists()
        {
            if (_sdk != null && _sdk.GetAttachedBusylightDeviceList().Length == 0)
            {
                var message = "No Busylight device.";
                Logger.GetLogger().Warn(message);
                Events.GetInstance().TriggerShowError(message);
                throw new ActionException(message);
            }
        }

        public abstract string Execute();
    }
}