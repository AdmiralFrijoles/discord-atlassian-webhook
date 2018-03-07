using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using DiscordAtlassianWebhook.Config;
using DiscordAtlassianWebhook.Config.Models;
using DiscordAtlassianWebhook.Discord.Models;
using DiscordAtlassianWebhook.Models;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Constants;
using Unosquare.Labs.EmbedIO.Modules;
using HttpListenerContext = Unosquare.Net.HttpListenerContext;

namespace DiscordAtlassianWebhook.Bitbucket
{
    public class BitbucketController : WebApiController
    {
        /*
        [WebApiHandler(HttpVerbs.Get, "/bitbucket/test")]
        public bool Test(WebServer server, HttpListenerContext context)
        {
            var msg = new Discord.Models.Message
            {
                Content = "test",
                Embeds = new List<Embed>
                {
                    new Embed
                    {
                        Color = Color.Lime,
                    }
                }
            };

            var discordConfig = Program.Container.GetInstance<ConfigReader>().LoadSection<DiscordConfig>();
            bool success = discordConfig.Webhooks.FirstOrDefault(i => i.Name == "source-control")?.PostMessage(msg) ?? false;

            return context.JsonResponse(JsonConvert.SerializeObject(success));
        }
        */

        [WebApiHandler(HttpVerbs.Post, "/bitbucket")]
        public async Task<bool> HandleEvent(WebServer server, HttpListenerContext context)
        {
            string eventKey = context.Request.Headers.GetValues("X-Event-Key")?.FirstOrDefault();
            if (eventKey == null)
            {
                return context.JsonExceptionResponse(new InvalidOperationException("Invalid event key."),
                    HttpStatusCode.BadRequest);
            }

            var eventParts = eventKey.Split(':');
            if (eventParts.Length != 2)
            {
                return context.JsonExceptionResponse(new InvalidOperationException("Invalid event key."),
                    HttpStatusCode.BadRequest);
            }

            string eventCategory = eventParts[0];
            string eventName = eventParts[1];

            var configReader = Program.Container.GetInstance<ConfigReader>();
            var eventConfig = configReader.LoadSection<BitbucketConfig>().Events
                .FirstOrDefault(i =>
                    i.Category.Equals(eventCategory, StringComparison.InvariantCultureIgnoreCase) &&
                    i.Name.Equals(eventName, StringComparison.InvariantCultureIgnoreCase));

            if (eventConfig != null && eventConfig.Enabled)
            {
                var handlerData = GetBitbucketEventHandler(eventCategory, eventName);
                if (!handlerData.IsValid)
                {
                    return context.JsonExceptionResponse(
                        new InvalidOperationException($"No event handler found for {eventKey}."),
                        HttpStatusCode.BadRequest);
                }

                var handlerInstance = Activator.CreateInstance(handlerData.HandlerClass);

                string eventPayload;
                using (var streamReader = new StreamReader(context.Request.InputStream))
                {
                    eventPayload = streamReader.ReadToEnd();
                }

                // This assumes the signature of the method. Probably should check the signature before getting to this point.
                var messages = (IEnumerable<Message>) handlerData.HandlerMethod.Invoke(handlerInstance, new object[] {eventPayload});
                var discordConfig = configReader.LoadSection<DiscordConfig>();
                var webhooks = discordConfig.GetWebhooks(eventConfig.Webhooks)?.ToList();
                if (messages != null && webhooks != null)
                {
                    await SendMessages(messages, webhooks);
                }
            }

            context.Response.StatusCode = (int) HttpStatusCode.NoContent;
            context.Response.OutputStream.Close();
            return true;
        }

        private static EventHandlerData GetBitbucketEventHandler(string eventCategory, string eventName)
        {
            var handlerClasses = ReflectionHelpers
                .GetTypesWithAttribute<BitbucketEventHandlerAttribute>(Assembly.GetEntryAssembly())
                .Where(h => h.Value.Any(a =>
                    a.EventCategory.Equals(eventCategory, StringComparison.InvariantCultureIgnoreCase)))
                .Select(i => i.Key);

            var handlerData = handlerClasses.Select(h => new
                {
                    HandlerClass = h,
                    EventMethods = ReflectionHelpers.GetMethodsWithAttribute<BitbucketEventAttribute>(h)
                })
                .Select(h => new EventHandlerData
                {
                    HandlerClass = h.HandlerClass,
                    HandlerMethod = h.EventMethods
                        .Where(m => m.Value
                            .Any(i => i.EventName.Equals(eventName, StringComparison.CurrentCultureIgnoreCase))
                        ).Select(i => i.Key)
                        .SingleOrDefault()
                })
                .SingleOrDefault();
            return handlerData;
        }

        private static async Task SendMessages(IEnumerable<Message> messages, IEnumerable<Webhook> webhooks)
        {
            await Task.WhenAll(webhooks.Select(w => w.PostMessagesAsync(messages)));
        }
    }
}