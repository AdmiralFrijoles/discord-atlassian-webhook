using System.Net;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Config.Models
{
    public class ServerConfig
    {
        public string Host { get; set; } = "0.0.0.0";

        [JsonIgnore]
        public IPAddress HostAddress
        {
            get
            {
                try
                {
                    return IPAddress.Parse(Host);
                }
                catch
                {
                    return IPAddress.Any;
                }
            }
        }

        public int Port { get; set; } = 9080;
    }
}