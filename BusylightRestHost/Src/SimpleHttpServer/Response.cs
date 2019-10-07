using System.Collections.Generic;
using System.Net;
using static BusylightRestHost.SimpleHttpServer.ContentType;

namespace BusylightRestHost.SimpleHttpServer
{
    public class Response
    {
        public string Body { get; set; }
        public int HttpCode { get; set; }
        public Dictionary<HttpRequestHeader, string> Headers { get; }
        public string ContentType { get; set; }

        public Response()
        {
            Headers = new Dictionary<HttpRequestHeader, string>();
            HttpCode = 200;
            Body = "";
        }

        public Response AddHeader(HttpRequestHeader name, string value)
        {
            Headers.Add(name, value);
            return this;
        }

        public static Response Respond500(string body = "")
        {
            return RespondCode(500, body);
        }

        public static Response Respond404()
        {
            return RespondCode(404);
        }

        public static Response RespondCode(int code, string body = "")
        {
            return new Response
            {
                Body = body,
                HttpCode = code
            };
        }

        public static Response RespondPlain(string body)
        {
            return Respond(body);
        }

        public static Response RespondJson(string body)
        {
            return Respond(body, Json);
        }

        public static Response RespondSvg(string result)
        {
            var body = $@"<svg viewBox='0 0 120 200' xmlns='http://www.w3.org/2000/svg'>
                       <text y='10' font-size='12' font-family='serif'><![CDATA[{result}]]></text>
                </svg>";
            return Respond(body, "image/svg+xml");
        }

        public static Response Respond(string body, string contentType = Plain)
        {
            return new Response
            {
                Body = body,
                ContentType = contentType
            };
        }
    }
}