using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace BusylightRestHost.SimpleHttpServer
{
    public enum HttpMethod
    {
        Get,
        Post
    }

    public delegate Response RequestHandler(HttpListenerRequest request, HttpListenerContext context);

    internal class Route
    {
        public HttpMethod Method { get; }
        public RequestHandler Handler { get; }

        public Route(HttpMethod method, RequestHandler handler)
        {
            Method = method;
            Handler = handler;
        }
    }

    public class Server
    {
        private readonly int _port;
        private readonly List<string> _addresses;
        private readonly HttpListener _listener;
        private readonly Logger _logger;
        private readonly Dictionary<string, Route> _routes;

        public Server(int port, string address = "localhost")
        {
            ValidateHttpListenerSupported();
            _logger = Logger.GetLogger();
            _addresses = new List<string> {address};
            _port = port;
            _listener = new HttpListener();
            _routes = new Dictionary<string, Route>();
        }

        private static void ValidateHttpListenerSupported()
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException(
                    "Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
            }
        }

        public void Start()
        {
            _listener.Start();
            ThreadPool.QueueUserWorkItem(o =>
            {
                _logger.Debug($"SimpleHttpServer running: Scheme http, address: {string.Join(", ", _addresses.ToArray())}, port: {_port}");
                CatchErrors(() =>
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem(ctx =>
                        {
                            if (!(ctx is HttpListenerContext context))
                            {
                                return;
                            }

                            CatchErrors(() => { HandleRequest(context); }, "Unexpected error");
                        }, _listener.GetContext());
                    }
                }, "Failed to handle request");
            });
        }

        private void HandleRequest(HttpListenerContext context)
        {
            try
            {
                var response = PrepareResponse(context);
                var buffer = Encoding.UTF8.GetBytes(response.Body);
                context.Response.StatusCode = response.HttpCode;
                context.Response.ContentLength64 = buffer.Length;
                context.Response.ContentType = response.ContentType;
                CatchErrors(() => { AppendHeaders(context, response); }, "Failed to append response headers");
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                LogResponse(context);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to handle request");
                context.Response.StatusCode = 500;
            }
            finally
            {
                context.Response.OutputStream.Flush();
                context.Response.OutputStream.Close();
            }
        }

        private static void AppendHeaders(HttpListenerContext context, Response response)
        {
            foreach (var header in response.Headers)
            {
                context.Response.Headers.Add(header.Key, header.Value);
            }
        }

        private void LogResponse(HttpListenerContext context)
        {
            var response = context.Response;
            var type = response.ContentType ?? response.Headers[HttpRequestHeader.ContentType] ?? "text/plain";
            var log =
                $"Serve '{context.Request.Url.AbsolutePath}' [HTTP {response.StatusCode}, size: {response.ContentLength64}, content-type: {type}]";
            _logger.Debug(log);
        }

        private Response PrepareResponse(HttpListenerContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            try
            {
                var route = FindRoute(path);
                var method = ParseMethod(context.Request.HttpMethod);
                if (route.Method == method)
                {
                    return route.Handler(context.Request, context);
                }

                _logger.Error($"No route for method '{context.Request.HttpMethod}' and path '{path}'.");
                return Response.Respond404();
            }
            catch (KeyNotFoundException e)
            {
                _logger.Error(e, $"Route {path} not exists.");
                return Response.Respond404();
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Failed to handle request {context.Request.HttpMethod} {path}.");
                return Response.Respond500(
                    $"Failed to handle request {context.Request.HttpMethod} {path}. Error: {e.Message}");
            }
        }

        private Route FindRoute(string path)
        {
            foreach (var route in _routes)
            {
                if (route.Key == path || route.Key == path + "/")
                {
                    return route.Value;
                }
            }

            throw new KeyNotFoundException();
        }

        private static HttpMethod ParseMethod(string httpMethod)
        {
            return (HttpMethod) Enum.Parse(typeof(HttpMethod), httpMethod, true);
        }

        private void CatchErrors(System.Action method, string message)
        {
            try
            {
                method.Invoke();
            }
            catch (Exception e)
            {
                _logger.Error(e, message);
            }
        }

        public void RegisterPath(HttpMethod method, string path, RequestHandler handler)
        {
            var validPath = path.EndsWith("/") ? path : path + "/";
            _routes.Add(path, new Route(method, handler));
            AddPrefix(validPath);
        }

        private void AddPrefix(string validPath)
        {
            foreach (var address in _addresses)
            {
                var url = new UriBuilder("http", address, _port, validPath).ToString();
                _listener.Prefixes.Add(url);
            }
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}