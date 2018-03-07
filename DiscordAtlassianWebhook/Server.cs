using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Constants;
using Unosquare.Labs.EmbedIO.Modules;

namespace DiscordAtlassianWebhook
{
    internal class Server
    {
        public int Port { get; private set; }
        public IPAddress Host { get; private set; }

        public bool IsRunning => _webServer.Listener.IsListening;

        private readonly WebServer _webServer;

        public Server(IPAddress host, int port)
        {
            Host = host;
            Port = port;
            string hostString = Host.Equals(IPAddress.Any) ? "+" : Host.ToString();
            _webServer = new WebServer($"http://{hostString}:{Port}/", RoutingStrategy.Regex);
        }

        public void Start()
        {
            if (IsRunning)
            {
                throw new InvalidOperationException("Server is already started.");
            }

            _webServer.RegisterModule(new WebApiModule());
            RegisterControllers();

#pragma warning disable 4014
            _webServer.RunAsync();
#pragma warning restore 4014
        }

        private void RegisterControllers()
        {
            var controllerTypes = Assembly.GetEntryAssembly().GetTypes()
                .Where(i => i.IsClass && i.IsSubclassOf(typeof(WebApiController)));

            foreach (var controllerType in controllerTypes)
            {
                Console.WriteLine("Registered controller: {0}", controllerType.Name);
                _webServer.Module<WebApiModule>().RegisterController(controllerType);
            }
        }
        
    }
}
