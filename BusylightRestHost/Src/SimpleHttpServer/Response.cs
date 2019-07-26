using System.Collections.Generic;
using System.Net;

namespace BusylightRestHost.SimpleHttpServer
{
    public class Response
    {
        public string Body { get; set; }

        public int HttpCode { get; set; }

        public Dictionary<HttpRequestHeader, string> Headers { get; }

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

        public static Response Respond500()
        {
            return RespondCode(500);
        }

        public static Response Respond404()
        {
            return RespondCode(404);
        }

        public static Response RespondCode(int code)
        {
            return new Response
            {
                HttpCode = code
            };
        }

        public static Response RespondPlain(string body)
        {
            return Respond(body);
        }

        public static Response RespondJson(string body)
        {
            return Respond(body, ContentType.Json);
        }

        public static Response Respond(string body, string contentType = ContentType.Plain)
        {
            return new Response
            {
                Body = body
            }.AddHeader(HttpRequestHeader.ContentType, contentType);
        }
    }
}