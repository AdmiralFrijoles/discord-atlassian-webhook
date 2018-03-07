using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Parent
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }


    }
}