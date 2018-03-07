using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Inline
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}