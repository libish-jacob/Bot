using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApplication.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text != null)
            {
                // Calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // Return our reply to the user
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            }
            else if (activity.Attachments.Any())
            {
                string attachment = string.Empty;
                foreach (var item in activity.Attachments)
                {
                    attachment += item.Name + ",";
                }

                // Return our reply to the user
                await context.PostAsync($"You sent attachment(s) {attachment}");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}