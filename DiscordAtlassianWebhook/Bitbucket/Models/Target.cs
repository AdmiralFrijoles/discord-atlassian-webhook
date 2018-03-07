using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class Target
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty("parents")]
        public List<Parent> Parents { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}