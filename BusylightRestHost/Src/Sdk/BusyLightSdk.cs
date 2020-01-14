using Busylight;

namespace BusylightRestHost.Sdk
{
    public class BusyLightSdk : ISdk
    {
        private SDK _sdk;

        public BusyLightSdk()
        {
            _sdk = new SDK(true);
            _sdk.OnBusylightChanged += OnBusylightChanged;
        }

        public void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            _sdk.Alert(color, clip, volume);
        }

        public void Alert(int red, int blue, int green, BusylightSoundClip clip, BusylightVolume volume)
        {
            _sdk.Alert(red, blue, green, clip, volume);
        }

        public void ColorWithFlash(int red, int blue, int green, int flashred, int flashblue, int flashgreen)
        {
            _sdk.ColorWithFlash(red, blue, green, flashred, flashblue, flashgreen);
        }

        public IBusylightDevice[] GetAttachedBusylightDeviceList()
        {
            return _sdk.GetAttachedBusylightDeviceList();
        }

        public void Jingle(BusylightColor color, BusylightJingleClip clip, BusylightVolume volume)
        {
            _sdk.Jingle(color, clip, volume);
        }

        public void Jingle(int red, int blue, int green, BusylightJingleClip clip, BusylightVolume volume)
        {
            _sdk.Jingle(red, blue, green, clip, volume);
        }

        public void Light(int red, int blue, int green)
        {
            _sdk.Light(red, blue, green);
        }

        public void Light(BusylightColor color)
        {
            _sdk.Light(color);
        }

        public void Pulse(BusylightColor color)
        {
            _sdk.Pulse(color);
        }

        public void Pulse(PulseSequence pulseSequence)
        {
            _sdk.Pulse(pulseSequence);
        }

        public void Pulse(int red, int blue, int green)
        {
            _sdk.Pulse(red, blue, green);
        }

        public void Blink(BusylightColor color, int ontime, int offtime)
        {
            _sdk.Blink(color, ontime, offtime);
        }

        public void Blink(int red, int blue, int green, int ontime, int offtime)
        {
            _sdk.Blink(red, blue, green, ontime, offtime);
        }

        public event BusylightChangedDelegate OnBusylightChanged;
    }
}