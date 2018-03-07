using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.PullRequest
{
    public class PullRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("source")]
        public Location Source { get; set; }

        [JsonProperty("destination")]
        public Location Destination { get; set; }

        [JsonProperty("merge_commit")]
        public Commit MergeCommit { get; set; }

        [JsonProperty("participants")]
        public List<User> Participants { get; set; }

        [JsonProperty("reviewers")]
        public List<User> Reviewers { get; set; }

        [JsonProperty("close_source_branch")]
        public bool CloseSourceBranch { get; set; }

        [JsonProperty("closed_by")]
        public User ClosedBy { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("updated_on")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }

    }
}