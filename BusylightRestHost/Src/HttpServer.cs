using System.IO;
using System.Linq;
using System.Net;
using BusylightRestHost.Action;
using BusylightRestHost.SimpleHttpServer;
using BusylightRestHost.Utils;

namespace BusylightRestHost
{
    public class HttpServer
    {
        private readonly Logger _logger;
        private readonly BusylightController _busylightController;
        private readonly Server _server;
        private const string StaticContentResource = "WebHttpRoot";

        public HttpServer()
        {
            _logger = Logger.GetLogger();
            _busylightController = new BusylightController();
            _server = new Server(ApplicationText.ServerPort, ApplicationText.ServerAddress);

            _server.RegisterPath(HttpMethod.Get, "/", WelcomeMessageHandler);
            _server.RegisterPath(HttpMethod.Post, "/action", RunActionHandler);
            RegisterStaticResources();
        }

        private static Response WelcomeMessageHandler(HttpListenerRequest request, HttpListenerContext context)
        {
            return Response.RespondPlain($"Busylight Rest Host - version: {Version.Get()}");
        }

        private Response RunActionHandler(HttpListenerRequest request, HttpListenerContext unused)
        {
            var actionDataJson = Streams.Read(request.InputStream);
            var busylightAction = ActionData.FromJson(actionDataJson);
            return Response.RespondJson(_busylightController.RunAction(busylightAction));
        }

        private void RegisterStaticResources()
        {
            foreach (var resource in Resources.AllResources())
            {
                var resourceName = resource.Key;
                var content = resource.Value;
                const string resourceNamePrefix = StaticContentResource + ".";
                // ReSharper disable once InvertIf
                if (resourceName.StartsWith(resourceNamePrefix))
                {
                    var name = ResourceNameToPath(resourceName.Replace(resourceNamePrefix, ""));
                    var extension = Path.GetExtension(name)?.ToLower().Remove(0, 1);
                    var contentType = ContentType.ForExtension(extension);
                    _logger.Debug("Registered static content: " + name);
                    _server.RegisterPath(HttpMethod.Get, "/" + name, ServeStaticContent(content, contentType));
                }
            }
        }

        private static RequestHandler ServeStaticContent(string content, string contentType)
        {
            return (request, context) => Response.Respond(content, contentType);
        }

        public static string ResourceNameToPath(string resourceName)
        {
            var parts = resourceName.Split('.');
            if (parts.Length == 1)
            {
                return parts[0];
            }

            var pathParts = parts.Take(parts.Length - 1).ToArray();
            var ext = parts[parts.Length - 1];
            return string.Join("/", pathParts) + "." + ext;
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}