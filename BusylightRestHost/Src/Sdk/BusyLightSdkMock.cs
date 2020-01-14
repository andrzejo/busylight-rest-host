using System;
using Busylight;
using BusylightRestHost.Sdk;

namespace BusylightRestHost
{
    class MockDevice : IBusylightDevice
    {
        public MockDevice()
        {
            HIDDevicePath = null;
            ProductID = "Busylight mock device";
            FirmwareRelease = "1.0";
            USBID = null;
            IsSoundSupported = true;
            IsLightSupported = true;
            IsInputEventSupported = false;
            IsJingleClipSupport = false;
        }

        public string HIDDevicePath { get; }
        public string ProductID { get; }
        public string FirmwareRelease { get; }
        public string USBID { get; }
        public bool IsSoundSupported { get; }
        public bool IsLightSupported { get; }
        public bool IsInputEventSupported { get; }
        public bool IsJingleClipSupport { get; }
    }

    public class BusyLightSdkMock : ISdk
    {
        public void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            log(message: $"Alert(color:{color}, clip:{clip}, volume:{volume})");
        }

        public void Alert(int red, int blue, int green, BusylightSoundClip clip, BusylightVolume volume)
        {
            log(message: $"Alert(R:{red}, B:{blue}, G:{green}, clip:{clip}, volume:{volume})");
        }

        public void ColorWithFlash(int red, int blue, int green, int flashred, int flashblue, int flashgreen)
        {
            log(message:
                $"ColorWithFlash(R:{red}, B:{blue}, G:{green}, flashred:{flashred}, flashblue:{flashblue}, flashgreen:{flashgreen})");
        }

        public IBusylightDevice[] GetAttachedBusylightDeviceList()
        {
            log(message: $"GetAttachedBusylightDeviceList()");
            var device = new MockDevice();
            return new IBusylightDevice[] {device};
        }

        public void Jingle(BusylightColor color, BusylightJingleClip clip, BusylightVolume volume)
        {
            log(message: $"Jingle(color:{color}, clip:{clip}, volume:{volume})");
        }

        public void Jingle(int red, int blue, int green, BusylightJingleClip clip, BusylightVolume volume)
        {
            log(message: $"Light(R:{red}, G:{blue}, B:{green})");
        }

        public void Light(int red, int blue, int green)
        {
            log(message: $"Light(R:{red}, G:{blue}, B:{green})");
        }

        public void Light(BusylightColor color)
        {
            log(message: $"Light(color:{color})");
        }

        public void Pulse(BusylightColor color)
        {
            log(message: $"Pulse(color:{color})");
        }

        public void Pulse(PulseSequence pulseSequence)
        {
            log(message: $"Pulse(pulseSequence:{pulseSequence})");
        }

        public void Pulse(int red, int blue, int green)
        {
            log(message: $"Pulse(R:{red}, G:{blue}, B:{green})");
        }

        public void Blink(BusylightColor color, int ontime, int offtime)
        {
            log(message: $"Blink(color:{color}, ontime:{ontime}, offtime:{offtime})");
        }

        public void Blink(int red, int blue, int green, int ontime, int offtime)
        {
            log(message: $"Blink(R:{red}, G:{blue}, B:{green}, ontime:{ontime}, offtime:{offtime})");
        }

        private void log(string message)
        {
            Logger.GetLogger().Debug("BusyLightLibMock: " + message);
        }

        public event BusylightChangedDelegate OnBusylightChanged;
    }
}