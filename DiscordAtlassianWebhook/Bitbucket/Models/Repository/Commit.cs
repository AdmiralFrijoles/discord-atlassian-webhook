using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Repository
{
    public class Commit
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("parents")]
        public List<Parent> Parents { get; set; }

        [JsonProperty("summary")]
        public CommitSummary Summary { get; set; }
    }
}