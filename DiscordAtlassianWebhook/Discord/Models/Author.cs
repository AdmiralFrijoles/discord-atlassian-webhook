using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Author
    {
        [JsonProperty("name"), MaxLength(256)]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
    }
}