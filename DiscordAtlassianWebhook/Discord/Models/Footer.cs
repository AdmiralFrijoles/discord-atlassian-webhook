using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Footer
    {
        [JsonProperty("text"), MaxLength(2048)] public string Text { get; set; } = null;
        [JsonProperty("icon_url")] public string IconUrl { get; set; } = null;
    }
}