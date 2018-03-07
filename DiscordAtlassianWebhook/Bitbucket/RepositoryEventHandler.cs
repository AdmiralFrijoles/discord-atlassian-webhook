using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DiscordAtlassianWebhook.Bitbucket.Models.Repository.Events;
using DiscordAtlassianWebhook.Discord.Models;
using Newtonsoft.Json;

namespace DiscordAtlassianWebhook.Bitbucket
{
    [BitbucketEventHandler("repo")]
    public class RepositoryEventHandler
    {
        [BitbucketEvent("push")]
        public IEnumerable<Message> OnPushEvent(string payload)
        {
            var eventData = JsonConvert.DeserializeObject<Push>(payload);

            var discordMessage = new Message
            {
                Embeds = new List<Embed>()
            };

            foreach (var change in eventData.Details.Changes)
            {
                var embed = new Embed
                {
                    Color = Color.FromArgb(0, 52, 137),
                    Author = new Author
                    {
                        Name = eventData.Actor.DisplayName,
                        IconUrl = eventData.Actor.Links["avatar"].Address,
                        Url = eventData.Actor.Links["html"].Address
                    },
                    Title = $"[{eventData.Repository.FullName}]",
                };

                if (change.Created && change.New.Type == "branch")
                {
                    embed.Url = change.Links["html"].Address;
                    embed.Title += $" Branch \"{change.New.Name}\" created.";
                    embed.Description = $"The branch [{change.New.Name}]({change.Links["html"].Address} \"{change.New.Name}\") has been created.";
                }
                else if (change.Closed && change.Old.Type == "branch")
                {
                    embed.Url = eventData.Repository.Links["html"].Address;
                    embed.Title += $" Branch \"{change.Old.Name}\" deleted.";
                    embed.Description = $"The branch **{change.Old.Name}** has been deleted.";
                }
                else if(change.Commits != null && change.Commits.Any())
                {
                    string branchName = change.New.Name;
                    int numCommits = change.Commits.Count;
                    embed.Url = change.Links["html"].Address;
                    embed.Title += $" Pushed {(change.HasMoreCommits ? "more than " : "")}{numCommits} commit{(numCommits == 1 ? "" : "s")} to {branchName}.";

                    var descriptionBuilder = new StringBuilder();
                    for (int i = 0; i < change.Commits.Count; i++)
                    {
                        bool isFirst = i == 0;
                        bool isLast = i == change.Commits.Count - 1;

                        var commit = change.Commits[i];

                        string title = commit.Summary.Raw.FirstLine().Trim();
                        string link = commit.Links["html"].Address;
                        string hash = commit.Hash.Truncate(7);
                        descriptionBuilder.AppendLine($"[`{hash}`]({link} \"{title}\")  {title}");
                    }

                    if (change.HasMoreCommits)
                    {
                        descriptionBuilder.AppendLine();
                        descriptionBuilder.AppendLine("There are more commits that are not displayed.");
                    }
                    embed.Description = descriptionBuilder.ToString();
                }

                discordMessage.Embeds.Add(embed);
            }

            return new List<Message> { discordMessage };
        }

        [BitbucketEvent("fork")]
        public IEnumerable<Message> OnForkEvent(string payload)
        {
            return null;
        }
    }
}