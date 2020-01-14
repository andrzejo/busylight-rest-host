using Busylight;

namespace BusylightRestHost.Sdk
{
    public interface ISdk
    {
        void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume);
        void Alert(int red, int blue, int green, BusylightSoundClip clip, BusylightVolume volume);
        void ColorWithFlash(int red, int blue, int green, int flashred, int flashblue, int flashgreen);
        IBusylightDevice[] GetAttachedBusylightDeviceList();
        void Jingle(BusylightColor color, BusylightJingleClip clip, BusylightVolume volume);
        void Jingle(int red, int blue, int green, BusylightJingleClip clip, BusylightVolume volume);
        void Light(int red, int blue, int green);
        void Light(BusylightColor color);
        void Pulse(BusylightColor color);
        void Pulse(PulseSequence pulseSequence);
        void Pulse(int red, int blue, int green);
        void Blink(BusylightColor color, int ontime, int offtime);
        void Blink(int red, int blue, int green, int ontime, int offtime);
        event BusylightChangedDelegate OnBusylightChanged;
    }
}