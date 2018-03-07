using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Author
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }
    }
}