using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Content
    {
        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("markup")]
        public string Markup { get; set; }
    }
}