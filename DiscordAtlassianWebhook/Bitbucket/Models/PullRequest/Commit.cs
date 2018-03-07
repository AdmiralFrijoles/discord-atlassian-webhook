using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.PullRequest
{
    public class Commit
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}