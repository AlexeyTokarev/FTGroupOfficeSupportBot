using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace FTGroupOfficeSupportBot.Dialogs
{
    public class CardDialog
    {
        /// <summary>
        /// Метод, определяющий площадку, на которой собирается работать пользователь
        /// </summary>
        /// <param name="context"></param>
        /// <param name="activity"></param>
        /// <param name="checkParametrs"></param>
        public static void OrganizationCard(IDialogContext context, Activity activity, string checkParametrs)
        {
            try
            {
                var replyToConversation = activity.CreateReply(); //(Activity)context.MakeMessage();
                replyToConversation.Attachments = new List<Attachment>();

                var cardButton = new List<CardAction>();
                var card1 = new CardAction()
                {
                    Value = "Финтендер",
                    Title = "Финтендер"
                };
                var card2 = new CardAction()
                {
                    Value = "РТС-Тендер",
                    Title = "РТС-Тендер"
                };

                cardButton.Add(card1);
                cardButton.Add(card2);

                var hero = new HeroCard()
                {
                    Buttons = cardButton,
                    Text = checkParametrs
                };
                var attach = hero.ToAttachment();
                //if (attach == null) throw new ArgumentNullException(nameof(attach));

                replyToConversation.Attachments.Add(attach);
                context.PostAsync(replyToConversation);
                // context.PostAsync("4");
            }
            catch (Exception e)
            {
                //context.PostAsync("3");
                context.PostAsync(e.Message + " Error code 1.");
            }
        }
    }
}