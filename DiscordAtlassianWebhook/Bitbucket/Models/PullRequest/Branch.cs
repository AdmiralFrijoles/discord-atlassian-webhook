using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.PullRequest
{
    public class Branch
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}