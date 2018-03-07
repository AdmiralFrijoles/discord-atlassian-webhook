using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Field
    {
        [JsonProperty("name"), MaxLength(256)]
        public string Name { get; set; }

        [JsonProperty("value"), MaxLength(1024)]
        public string Value { get; set; }

        [JsonProperty("inline")]
        public bool Inline { get; set; }
    }
}