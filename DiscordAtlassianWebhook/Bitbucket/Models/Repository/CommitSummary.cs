using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Repository
{
    public class CommitSummary
    {
        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("markup")]
        public string Markup { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}