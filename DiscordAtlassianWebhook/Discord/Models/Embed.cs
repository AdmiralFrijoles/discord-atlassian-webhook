using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Embed
    {
        [JsonProperty("title"), MaxLength(256)] public string Title { get; set; }

        [JsonProperty("description"), MaxLength(2048)] public string Description { get; set; } = null;

        [JsonProperty("url")] public string Url { get; set; } = null;

        [JsonIgnore] public DateTime? Timestamp { get; set; } = null;
        [JsonProperty("timestamp")] protected string TimestampISO => Timestamp?.ToString("O");

        [JsonIgnore] public Color? Color { get; set; } = null;
        [JsonProperty("color")] protected int? ColorInt => Color?.ToInteger();

        [JsonProperty("footer")] public Footer Footer { get; set; } = null;

        [JsonProperty("image")] public Image Image { get; set; } = null;

        [JsonProperty("thumbnail")] public Thumbnail Thumbnail { get; set; } = null;

        [JsonProperty("author")] public Author Author { get; set; } = null;

        [JsonProperty("fields"), MaxLength(25)] public List<Field> Fields { get; set; } = null;
    }
}