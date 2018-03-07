using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.PullRequest
{
    public class Location
    {
        [JsonProperty("branch")]
        public Branch Branch { get; set; }

        [JsonProperty("commit")]
        public Commit Commit { get; set; }

        [JsonProperty("repository")]
        public Repository.Repository Repository { get; set; }
    }
}