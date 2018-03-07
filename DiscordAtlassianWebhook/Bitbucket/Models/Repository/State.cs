using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Repository
{
    public class State
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }
    }
}