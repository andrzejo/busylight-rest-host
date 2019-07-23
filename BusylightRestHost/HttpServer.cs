using System.IO;
using System.Net;
using System.Reflection;

namespace BusylightRestHost
{
    public class HttpServer : Qoollo.Net.Http.HttpServer
    {
        private readonly Logger logger;
        private BusylightController busylightController;
        private const string StaticContentResource = "web";

        public HttpServer() : base(5748)
        {
            logger = Logger.GetLogger();
            busylightController = new BusylightController();
            Get["/"] = request => "Busylight Rest Host";
            Post["/action"] = RunAction;
            RegisterStaticResources();
        }

        private string RunAction(HttpListenerRequest arg)
        {
            var action = ReadStream(arg.InputStream);
            return busylightController.RunAction(BusylightAction.FromJson(action));
        }

        public void Start()
        {
            Run();
        }

        private void RegisterStaticResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var currentNamespace = typeof(HttpServer).Namespace;

            foreach (var resourceName in resourceNames)
            {
                var resourceNamePrefix = currentNamespace + "." + StaticContentResource + ".";
                if (resourceName.StartsWith(resourceNamePrefix))
                {
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        var result = ReadStream(stream);
                        var name = resourceName.Replace(resourceNamePrefix, "");
                        logger.Debug("Registered static content: " + name);
                        Get["/" + name] = _ => result;
                    }
                }
            }
        }

        private static string ReadStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}