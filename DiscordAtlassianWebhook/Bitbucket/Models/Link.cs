using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Link
    {
        [JsonProperty("href")]
        public string Address { get; set; }
    }
}