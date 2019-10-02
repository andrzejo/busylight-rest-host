using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Busylight;

namespace BusylightRestHost.Action
{
    public class ActionParameters
    {
        private readonly Dictionary<string, string> _parameters;

        public ActionParameters(Dictionary<string, string> parameters)
        {
            _parameters = parameters;
        }

        public string GetValue(string parameter)
        {
            return _parameters.TryGetValue(parameter, out var value) ? value : null;
        }

        public BusylightColor GetColor(string parameter = "color")
        {
            var color = GetValue(parameter);
            try
            {
                var rx = new Regex(@"\#(?<r>\w{2})(?<g>\w{2})(?<b>\w{2})",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var matches = rx.Matches(color);
                if (matches.Count > 0 && matches[0].Groups.Count == 4)
                {
                    var busylightColor = new BusylightColor
                    {
                        RedRgbValue = Convert.ToInt32(matches[0].Groups["r"].Value, 16),
                        GreenRgbValue = Convert.ToInt32(matches[0].Groups["g"].Value, 16),
                        BlueRgbValue = Convert.ToInt32(matches[0].Groups["b"].Value, 16)
                    };
                    return busylightColor;
                }
            }
            catch (Exception e)
            {
                throw new ActionException($"Failed to parse color {color}.", e);
            }

            throw new ActionException($"Invalid color value '{color}'.");
        }

        public BusylightSoundClip GetSound(string parameter = "sound")
        {
            var sound = GetValue(parameter);
            if (Enum.TryParse(sound, out BusylightSoundClip clip))
            {
                return clip;
            }

            var sounds = string.Join(", ", Enum.GetNames(typeof(BusylightSoundClip)));
            throw new ActionException($"Invalid sound parameter value: '{sound}'. Sound must be one of: '{sounds}'.");
        }

        public BusylightVolume GetVolume(string parameter = "volume")
        {
            var soundVolume = GetValue(parameter);
            if (Enum.TryParse(soundVolume, out BusylightVolume volume))
            {
                return volume;
            }

            var volumes = string.Join(", ", Enum.GetNames(typeof(BusylightVolume)));
            throw new ActionException(
                $"Invalid sound parameter value: '{soundVolume}'. Sound must be one of: '{volumes}'.");
        }

        public int GetInt(string parameter = "number")
        {
            var intValue = GetValue(parameter);
            if (int.TryParse(intValue, out var value))
            {
                return value;
            }

            throw new ActionException(
                $"Invalid parameter value: '{intValue}'. Parameter '{parameter}' must be valid number.");
        }

        public override string ToString()
        {
            var toString = "BusylightRestHost.Action.ActionParameters(";
            foreach (var parameter in _parameters)
            {
                toString += parameter.Key + ": '" + parameter.Value + "', ";
            }

            char[] charsToTrim = {',', ' '};
            return toString.Trim(charsToTrim) + ")";
        }
    }
}