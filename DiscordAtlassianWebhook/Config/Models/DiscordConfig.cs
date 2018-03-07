using DiscordAtlassianWebhook.Discord.Models;

namespace DiscordAtlassianWebhook.Config.Models
{
    public class DiscordConfig
    {
        public string BotName { get; set; }
        public string AvatarUrl { get; set; }
        public Webhook[] Webhooks { get; set; }
    }
}