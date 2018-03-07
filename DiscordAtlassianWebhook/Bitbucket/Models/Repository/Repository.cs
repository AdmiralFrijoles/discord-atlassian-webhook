using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Repository
{
    public class Repository
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("uuid")]
        public string RepoId { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("owner")]
        public User Owner { get; set; }

        [JsonProperty("scm")]
        public string SourceControlModule { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }
    }
}