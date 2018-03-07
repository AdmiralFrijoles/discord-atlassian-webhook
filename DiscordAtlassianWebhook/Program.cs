using System.Threading;
using DiscordAtlassianWebhook.Config;
using DiscordAtlassianWebhook.Config.Models;
using SimpleInjector;

namespace DiscordAtlassianWebhook
{
    internal class Program
    {
        internal static readonly Container Container;

        static Program()
        {
            Container = new Container();
            Container.Register(() => new ConfigReader("config.json"), Lifestyle.Singleton);
            Container.Verify();
        }

        private static void Main(string[] args)
        {
            var serverConfig = Container.GetInstance<ConfigReader>().LoadSection<ServerConfig>();

            var server = new Server(serverConfig.HostAddress, serverConfig.Port);
            server.Start();

            while (server.IsRunning)
            {
                Thread.Sleep(10);
            }
        }
    }
}
