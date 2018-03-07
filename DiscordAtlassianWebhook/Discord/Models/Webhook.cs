using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DiscordAtlassianWebhook.Config;
using DiscordAtlassianWebhook.Config.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DiscordAtlassianWebhook.Discord.Models
{
    public class Webhook
    {
        public string Name { get; protected set; }
        public string URL { get; protected set; }

        [JsonConstructor]
        protected Webhook()
        {
        }
        public Webhook(string name, string url)
        {
            Name = name;
            URL = url;
        }

        private IRestClient _restClient;
        private IRestClient RestClient => _restClient ?? (_restClient = new RestClient(URL));

        private IRestRequest CreatePostMessageRequest(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            try
            {
                string messageJson = JsonConvert.SerializeObject(message);
                var request = new RestRequest(Method.POST);
                request.AddParameter("application/json", messageJson, ParameterType.RequestBody);
                return request;
            }
            catch (JsonSerializationException)
            {
                return null;
            }
        }

        public async Task<bool> PostMessageAsync(Message message)
        {
            var discordConfig = Program.Container.GetInstance<ConfigReader>().LoadSection<DiscordConfig>();
            if (!string.IsNullOrWhiteSpace(discordConfig.BotName))
            {
                message.Username = discordConfig.BotName;
            }
            if (!string.IsNullOrWhiteSpace(discordConfig.AvatarUrl))
            {
                message.AvatarUrl = discordConfig.AvatarUrl;
            }

            var request = CreatePostMessageRequest(message);
            if (request == null)
            {
                return false;
            }
            var response = await RestClient.ExecuteTaskAsync(request);
            bool success = response.IsSuccessful &&
                   response.StatusCode == HttpStatusCode.NoContent;

            // Too fast. Thankfully, Discord tells us exactly when to retry.
            if (!success && (int) response.StatusCode == 429)
            {
                try
                {
                    var contentJson = JObject.Parse(response.Content);
                    int retryMilliseconds = contentJson["retry_after"].ToObject<int>();
                    await Task.Delay(retryMilliseconds + 100);
                    return await PostMessageAsync(message);
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public async Task PostMessagesAsync(IEnumerable<Message> messages)
        {
            // This could definitely better respect Discord's rate limits.
            await Task.WhenAll(messages.Select(PostMessageAsync));
        } 
    }
}