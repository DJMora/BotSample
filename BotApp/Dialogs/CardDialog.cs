using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotApp.Dialogs
{
    [Serializable]
    public class CardDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> arg)
        {
            var replyToConversation = context.MakeMessage();

            replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyToConversation.Attachments = new List<Attachment>();
            List<CardImage> CardImages = new List<CardImage>();
            CardImages.Add(new CardImage()
            {
                Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/BMW-Z4_diagonal_front_at_IAA_2005.jpg/243px-BMW-Z4_diagonal_front_at_IAA_2005.jpg"
            });

            CardAction btnWebsite = new CardAction()
            {
                Type = "openUrl",
                Title = "Open",
                Value = "http://bmw.com"
            };

            HeroCard plCard = new HeroCard()
            {
                Title = $"Title",
                Subtitle = $"Resultados de busqueda para",
                Images = CardImages,
                Tap = btnWebsite
            };

            var attachment = plCard.ToAttachment();
            replyToConversation.Attachments.Add(attachment);
            await context.PostAsync(replyToConversation);

            //var connector = new ConnectorClient(new Uri(msg.ServiceUrl));
            //var reply = connector.Conversations.SendToConversationAsync(replyToConversation);
        }
    }
}