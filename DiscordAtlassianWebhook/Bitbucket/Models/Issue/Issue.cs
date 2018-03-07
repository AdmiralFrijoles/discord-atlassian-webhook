using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Issue
{
    public class Issue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("component")]
        public string Component { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("milestone")]
        public Milestone Milestone { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updated_on")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }
    }
}