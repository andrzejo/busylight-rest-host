using System;
using System.IO;
using System.Net;
using BusylightRestHost.Action;
using BusylightRestHost.Utils;
using Qoollo.Net.Http;

namespace BusylightRestHost
{
    public class HttpServer : Qoollo.Net.Http.HttpServer
    {
        private readonly Logger _logger;
        private readonly BusylightController _busylightController;
        private const string StaticContentResource = "web";

        public HttpServer() : base(5748)
        {
            _logger = Logger.GetLogger();
            _busylightController = new BusylightController();
            RegisterAction(Get, "WelcomeMessage", "/", WelcomeMessage);
            RegisterAction(Post, "RunAction", "/action", RunAction);

            RegisterStaticResources();
        }

        private static string WelcomeMessage(HttpListenerRequest arg)
        {
            return $"Busylight Rest Host - version: {Version.Get()}";
        }

        private void RegisterAction(RequestHandlerRegistrator method, string name, string path,
            Func<HttpListenerRequest, string> handler)
        {
            method[path] = _ => WrapHttpMethod(name, _, handler);
        }

        private string RunAction(HttpListenerRequest arg)
        {
            var actionDataJson = Streams.Read(arg.InputStream);
            var busylightAction = ActionData.FromJson(actionDataJson);
            return _busylightController.RunAction(busylightAction);
        }

        private string WrapHttpMethod(string name, HttpListenerRequest request,
            Func<HttpListenerRequest, string> method)
        {
            try
            {
                return method.Invoke(request);
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to execute http action '{name}': {e.Message}. Trace: {e.StackTrace}");
                throw e;
            }
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
                    var name = resourceName.Replace(resourceNamePrefix, "");
                    _logger.Debug("Registered static content: " + name);
                    RegisterAction(Get, $"ServeStaticContent({name})", "/" + name, _ => content);
                }
            }
        }

        public void Start()
        {
            Run();
        }
    }
}