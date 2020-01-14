using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Busylight;
using BusylightRestHost.Sdk;
using BusylightRestHost.Utils;

namespace BusylightRestHost
{
    public interface IBusylightLibProvider
    {
        ISdk Instance();
    }

    public class BusylightLibProvider : IBusylightLibProvider
    {
        private static ISdk _sdk;
        private static int _devicesCount = 0;

        public ISdk Instance()
        {
            if (_sdk == null)
            {
                var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                var forceMock = AppArguments.forceMock();
                if (isWindows && !forceMock)
                {
                    try
                    {
                        Logger.GetLogger().Info("Windows platform detected, try to setup BusylightSDK.");
                        _sdk = new BusyLightSdk();
                        _sdk.OnBusylightChanged += BusyLightChanged;
                    }
                    catch (Exception e)
                    {
                        Logger.GetLogger().Error(e, "Failed to create BusylightSDK.");
                    }
                }
                else
                {
                    Logger.GetLogger().Info(forceMock
                        ? "--force-mock argument passed, setup BusylightSDK mock."
                        : "Non-Windows platform detected, setup BusylightSDK mock.");
                    _sdk = new BusyLightSdkMock();
                    _sdk.OnBusylightChanged += BusyLightChanged;
                    BusyLightChanged();
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