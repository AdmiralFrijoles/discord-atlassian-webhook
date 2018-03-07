using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Project
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uuid")]
        public string ProjectId { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}