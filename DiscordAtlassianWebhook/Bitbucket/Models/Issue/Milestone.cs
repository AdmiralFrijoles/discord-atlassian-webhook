using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Issue
{
    public class Milestone
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}