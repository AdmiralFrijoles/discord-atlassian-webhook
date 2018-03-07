using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Image
    {
        [JsonProperty("url")] public string Url { get; set; } = null;
    }
}