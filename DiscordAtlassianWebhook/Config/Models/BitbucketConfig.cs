// ReSharper disable InconsistentNaming

namespace DiscordAtlassianWebhook.Config.Models
{
    public class BitbucketConfig
    {
        public class BitbucketEvent
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public string[] Webhooks { get; set; }
        }

        public BitbucketEvent[] Events { get; set; }
    }
}