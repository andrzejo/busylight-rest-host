using System.Reflection;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Modules;

namespace BusylightRestHost
{
    public class HttpServer
    {
        public HttpServer()
        {
        }

        public async void start()
        {
            using (var server = new WebServer("http://127.0.0.1:5748"))
            {
                var assembly = typeof(Program).Assembly;
                server.RegisterModule(new ResourceFilesModule(assembly, "bl_host.web"));
                server.RegisterModule(new WebApiModule());
                server.Module<WebApiModule>().RegisterController<BusylightController>();
                await server.RunAsync();
            }
        }
    }
}