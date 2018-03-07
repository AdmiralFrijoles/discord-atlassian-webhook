using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models
{
    public class User
    {
        [JsonProperty("type")]
        public string UserType { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("uuid")]
        public string UserId { get; set; }
        [JsonProperty("links")]
        public Dictionary<string, Link> Links { get; set; }
    }
}