using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket.Models.Repository.Events
{
    public class Push
    {
        public class PushDetails
        {
            [JsonProperty("changes", Required = Required.Always)]
            public List<Change> Changes { get; set; }
        }

        [JsonProperty("actor", Required = Required.Always)]
        public User Actor { get; set; }

        [JsonProperty("repository", Required = Required.Always)]
        public Repository Repository { get; set; }

        [JsonProperty("push", Required = Required.Always)]
        public PushDetails Details { get; set; }
    }
}