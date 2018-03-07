using System;

namespace DiscordAtlassianWebhook.Bitbucket
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class BitbucketEventHandlerAttribute : Attribute
    {
        public string EventCategory { get; }

        public BitbucketEventHandlerAttribute(string eventCategory)
        {
            EventCategory = eventCategory;
        }
    }
}