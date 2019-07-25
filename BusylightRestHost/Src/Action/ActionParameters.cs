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
                throw new ArgumentException($"Failed to parse color {color}.", e);
            }

            throw new ArgumentException($"Invalid color value '{color}'.");
        }
    }
}