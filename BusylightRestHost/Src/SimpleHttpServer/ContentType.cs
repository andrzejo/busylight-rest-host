using System.Collections.Generic;

namespace BusylightRestHost.SimpleHttpServer
{
    public static class ContentType
    {
        public const string Html = "text/html";
        public const string Plain = "text/plain";
        public const string Js = "application/javascript";
        public const string Json = "application/json";
        public const string Css = "text/css";


        private static readonly Dictionary<string, string> ExtContentTypes = new Dictionary<string, string>
        {
            {"js", Js},
            {"html", Html},
            {"txt", Plain},
            {"json", Json},
            {"css", Css}
        };

        public static string ForExtension(string extension)
        {
            return ExtContentTypes.TryGetValue(extension, out var type) ? type : Html;
        }
    }
}