using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Busylight;
using BusylightRestHost.Sdk;
using Timer = System.Timers.Timer;


namespace BusylightRestHost.Sdk
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
        private readonly Form _form;
        private readonly Label _label;
        private Timer _timer;

        public BusyLightSdkMock()
        {
            _form = new Form {Text = "Busylight Mock Window", BackColor = Color.White};
            _label = new Label {AutoSize = true};
            _form.Controls.Add(_label);
            _form.Show();
            _form.FormClosing += (sender, args) => { args.Cancel = args.CloseReason != CloseReason.ApplicationExitCall; };
        }

        private void SetupTimer(System.Action step1, int interval, System.Action step2)
        {
            _timer?.Close();
            _timer = new Timer();
            int tick = 0;
            step1.Invoke();
            _timer.Elapsed += (sender, args) =>
            {
                tick++;
                Logger.GetLogger().Info($"Tick {tick}");
                if (tick % 2 == 0)
                {
                    step1.Invoke();
                }
                else
                {
                    step2.Invoke();
                }
            };
            _timer.Interval = interval;
            _timer.Enabled = true;
            _timer.AutoReset = true;
        }

        public void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            Action(message: $"Alert(color:{color}, clip:{clip}, volume:{volume})");
            SetupTimer(() => { SetColor(color); }, 500, () => SetColor(0xff, 0xff, 0xff));
        }

        public void Alert(int red, int blue, int green, BusylightSoundClip clip, BusylightVolume volume)
        {
            Action(message: $"Alert(R:{red}, B:{blue}, G:{green}, clip:{clip}, volume:{volume})");
            SetupTimer(() => { SetColor(red, blue, green); }, 500, () => SetColor(0xff, 0xff, 0xff));
        }

        public void ColorWithFlash(int red, int blue, int green, int flashred, int flashblue, int flashgreen)
        {
            Action(message:
                $"ColorWithFlash(R:{red}, B:{blue}, G:{green}, flashred:{flashred}, flashblue:{flashblue}, flashgreen:{flashgreen})");
            SetupTimer(() => { SetColor(red, blue, green); }, 100, () => SetColor(flashred, flashblue, flashgreen));
        }

        public IBusylightDevice[] GetAttachedBusylightDeviceList()
        {
            Action(message: $"GetAttachedBusylightDeviceList()");
            var device = new MockDevice();
            return new IBusylightDevice[] {device};
        }

        public void Jingle(BusylightColor color, BusylightJingleClip clip, BusylightVolume volume)
        {
            Action(message: $"Jingle(color:{color}, clip:{clip}, volume:{volume})");
            SetColor(color);
        }

        public void Jingle(int red, int blue, int green, BusylightJingleClip clip, BusylightVolume volume)
        {
            Action(message: $"Light(R:{red}, G:{blue}, B:{green})");
            SetColor(red, blue, green);
        }

        public void Light(int red, int blue, int green)
        {
            Action(message: $"Light(R:{red}, G:{blue}, B:{green})");
            SetColor(red, blue, green);
        }

        public void Light(BusylightColor color)
        {
            Action(message: $"Light(color:{color})");
            SetColor(color);
        }

        public void Pulse(BusylightColor color)
        {
            Action(message: $"Pulse(color:{color})");
            SetColor(color);
        }

        public void Pulse(PulseSequence pulseSequence)
        {
            Action(message: $"Pulse(pulseSequence:{pulseSequence})");
        }

        public void Pulse(int red, int blue, int green)
        {
            Action(message: $"Pulse(R:{red}, G:{blue}, B:{green})");
            SetColor(red, blue, green);
        }

        public void Blink(BusylightColor color, int ontime, int offtime)
        {
            Action(message: $"Blink(color:{color}, ontime:{ontime}, offtime:{offtime})");
            SetupTimer(() => { SetColor(color); }, ontime, () => SetColor(0xff, 0xff, 0xff));
        }

        public void Blink(int red, int blue, int green, int ontime, int offtime)
        {
            Action(message: $"Blink(R:{red}, G:{blue}, B:{green}, ontime:{ontime}, offtime:{offtime})");
            SetupTimer(() => { SetColor(red, blue, green); }, ontime, () => SetColor(0xff, 0xff, 0xff));
        }

        private void SetColor(int red, int blue, int green)
        {
            _form.BackColor = Color.FromArgb(red, green, blue);
        }

        private void SetColor(BusylightColor color)
        {
            SetColor(color.RedRgbValue, color.BlueRgbValue, color.GreenRgbValue);
        }

        private void Action(string message)
        {
            _timer?.Close();
            Logger.GetLogger().Debug("BusyLightLibMock: " + message);
            _label.Text = message;
            _form.TopMost = true;
            _form.Visible = true;
        }

        public event BusylightChangedDelegate OnBusylightChanged;
    }
}