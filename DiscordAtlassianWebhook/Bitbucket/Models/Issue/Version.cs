using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Issue
{
    public class Version
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}