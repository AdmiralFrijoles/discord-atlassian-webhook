using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Message
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("tts")]
        public bool TextToSpeech { get; set; }

        [JsonProperty("embeds")]
        public List<Embed> Embeds { get; set; }
    }
}