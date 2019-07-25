using System;
using Busylight;

namespace BusylightRestHost
{
    public interface IBusylightLibProvider
    {
        ISDK Instance();
    }

    public class BusylightLibProvider : IBusylightLibProvider
    {
        private SDK _sdk;

        public ISDK Instance()
        {
            if (_sdk == null)
            {
                try
                {
                    _sdk = new SDK();
                }
                catch (Exception e)
                {
                    Logger.GetLogger().Error(e, "Failed to create BusylightSDK.");
                }
            }

            return _sdk;
        }
    }
}