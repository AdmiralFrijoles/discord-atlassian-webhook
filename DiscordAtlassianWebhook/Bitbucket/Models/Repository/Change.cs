using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Repository
{
    public class Change
    {
        [JsonProperty("new", Required = Required.AllowNull)]
        public State New { get; set; }

        [JsonProperty("old", Required = Required.AllowNull)]
        public State Old { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty("created")]
        public bool Created { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("forced")]
        public bool Forced { get; set; }

        [JsonProperty("truncated")]
        public bool HasMoreCommits { get; set; }

        [JsonProperty("commits")]
        public List<Commit> Commits { get; set; }
    }
}