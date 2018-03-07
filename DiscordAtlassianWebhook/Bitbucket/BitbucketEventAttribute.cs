using System;

namespace DiscordAtlassianWebhook.Bitbucket
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class BitbucketEventAttribute : Attribute
    {
        public string EventName { get; }

        public BitbucketEventAttribute(string eventName)
        {
            EventName = eventName;
        }
    }
}