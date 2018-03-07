using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Thumbnail
    {
        [JsonProperty("url")] public string Url { get; set; }
    }
}