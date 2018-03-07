using System;

namespace DiscordAtlassianWebhook.Config
{
    public interface IConfigReader
    {
        T Load<T>() where T : class, new();
        T LoadSection<T>() where T : class, new();
        object Load(Type type);
        object LoadSection(Type type);
    }
}