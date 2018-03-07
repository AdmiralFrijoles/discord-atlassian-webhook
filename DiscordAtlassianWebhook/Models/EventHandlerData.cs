using System;
using System.Reflection;

namespace DiscordAtlassianWebhook.Models
{
    public struct EventHandlerData
    {
        public Type HandlerClass;
        public MethodInfo HandlerMethod;

        public bool IsValid => HandlerClass != null && HandlerMethod != null;

        public EventHandlerData(Type handlerClass, MethodInfo methodInfo)
        {
            HandlerClass = handlerClass;
            HandlerMethod = methodInfo;
        }
    }
}