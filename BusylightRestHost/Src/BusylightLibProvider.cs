using System;
using System.Collections.Generic;
using System.Linq;
using Busylight;

namespace BusylightRestHost
{
    public interface IBusylightLibProvider
    {
        SDK Instance();
    }

    public class BusylightLibProvider : IBusylightLibProvider
    {
        private static SDK _sdk;
        private static int _devicesCount = 0;

        public SDK Instance()
        {
            if (_sdk == null)
            {
                try
                {
                    _sdk = new SDK(true);
                    _sdk.OnBusylightChanged += BusyLightChanged;
                }
                catch (Exception e)
                {
                    Logger.GetLogger().Error(e, "Failed to create BusylightSDK.");
                }
            }

            return _sdk;
        }

        private void BusyLightChanged()
        {
            var list = _sdk.GetAttachedBusylightDeviceList();
            List<string> names = list.Select(device => device.ProductID + " ver " + device.FirmwareRelease).ToList();
            var devices = string.Join(", ", names.ToArray());
            Logger.GetLogger().Debug("BusyLightChanged " + devices);
            
            var message = (list.Length > _devicesCount) ? "Connected " + names.Last() : "Disconnected Busylight device";
            _devicesCount = list.Length;
            Events.GetInstance().TriggerDeviceChanged(message);
        }
    }
}