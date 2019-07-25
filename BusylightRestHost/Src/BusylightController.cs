using System;
using Busylight;
using BusylightRestHost.Action;


namespace BusylightRestHost
{
    public class BusylightController
    {
        private readonly Factory _factory;

        public BusylightController()
        {
            try
            {
                _factory = new Factory(new SDK());
            }
            catch (Exception e)
            {
                Logger.GetLogger().Error(e, "Failed to create BusylightSDK.");
            }
        }

        public string RunAction(ActionData actionData)
        {
            return _factory == null ? "" : _factory.Create(actionData).Execute();
        }
    }
}