using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using Busylight;

namespace bl_host
{
    [DataContract]
    public class BusylightAction
    {
        [DataMember] public String action;
        [DataMember] public Dictionary<String, String> parameters = new Dictionary<string, string>();

        public BusylightAction()
        {
            action = "color";
            parameters["color"] = "#ff0000";
        }

        public static BusylightAction FromJson(string actionDataJson)
        {
            var stream = new MemoryStream(Encoding.Unicode.GetBytes(actionDataJson)) {Position = 0};
            var serializer = new DataContractJsonSerializer(typeof(BusylightAction));
            return (BusylightAction) serializer.ReadObject(stream);
        }

        public string GetAction()
        {
            return action;
        }

        public string GetParam(string name)
        {
            return parameters.TryGetValue(name, out var value) ? value : null;
        }

        public BusylightColor GetColorParam()
        {
            try
            {
                var color = GetParam("color");
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
            }

            return BusylightColor.Off;
        }
    }
}