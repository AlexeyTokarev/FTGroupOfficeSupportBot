using ApiAi;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace FTGroupOfficeSupportBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string _organization; // Проверка организации, интересующей пользователя ("РТС-Тендер", "Финтендер")
        private bool _parametrs; // Быстрая проверка наличия всех параметров

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            try
            {
                if (ResetParametrs.Reset(activity?.Text))
                {
                    _organization = null;
                    _parametrs = false;
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);
            }

            if (_parametrs == false)
            {
                if (string.IsNullOrEmpty(_organization)) 
                {
                    if (!string.IsNullOrWhiteSpace(activity?.Text))
                    {
                        var apiAiResponse = ApiAiRequest.ApiAiBotRequest(activity.Text);

                        // Если есть ошибки
                        if (apiAiResponse.Errors != null && apiAiResponse.Errors.Count > 0)
                        {
                            await context.PostAsync("Что-то пошло не так, повторите попытку");
                        }

                        // Если нет ошибок
                        else
                        {
                            // Проверка наличия, добавление или редактирование параметра "Организация"
                            if (!string.IsNullOrEmpty(_organization))
                            {
                                if ((_organization != apiAiResponse.Organization) &&
                                    (!string.IsNullOrEmpty(apiAiResponse.Organization)))
                                {
                                    _organization = apiAiResponse.Organization;
                                }
                            }
                            else
                            {
                                _organization = apiAiResponse.Organization;
                            }
                        }
                    }
                    else
                    {
                        await context.PostAsync("Что-то пошло не так, повторите попытку");
                    }

                    // Идет проверка наличия всех заполненных и незаполненных параметров с последующим информированием пользователя
                    if (string.IsNullOrEmpty(_organization))
                    {
                        string checkParametrs = ParametrsDialog.CheckParametrs(_organization);

                        if (string.IsNullOrEmpty(_organization))
                        {
                            try
                            {
                                //await context.PostAsync("1");
                                CardDialog.OrganizationCard(context, activity, checkParametrs);
                                //await context.PostAsync("5");
                            }
                            catch (Exception ex)
                            {
                                //await context.PostAsync("2");
                                await context.PostAsync(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        _parametrs = true;
                        await context.PostAsync(
                            "Напишите теперь интересующую Вас тему. Для возврата в исходное состояние наберите слово \"сброс\"");
                        activity.Text = null;
                    }
                }
                else
                {
                    _parametrs = true;
                    await context.PostAsync("Напишите теперь интересующую Вас тему.");
                }
            }

            if (!string.IsNullOrEmpty(activity?.Text) && _parametrs)
            {
                var answer = new QnADialog().QnABotResponse(_organization, activity.Text);

                // Проверка длины сообщения. Делается потому, как некоторые мессенджеры имеют ограничения на длину сообщения
                if (answer.Length > 3500)
                {
                    while (answer.Length > 3500)
                    {
                        var substringPoint = 3500;

                        // Данный цикл обрабатывает возможность корректного разделения больших сообщений на более мелкие
                        // Причем разделение проводится по предложениям (Ориентиром является точка)
                        while (answer[substringPoint] != '.')
                        {
                            substringPoint--;
                        }

                        var subanswer = answer.Substring(0, substringPoint + 1);

                        await context.PostAsync(subanswer);
                        answer = answer.Remove(0, substringPoint + 1);
                    }
                    await context.PostAsync(answer);
                }
                else
                {
                    await context.PostAsync(answer);
                }
            }
            //context.Wait(MessageReceivedAsync);
        }
    }
}