﻿using System.Collections.Generic;

namespace FTGroupOfficeSupportBot.Dialogs
{
    public class ResetParametrs
    {
        public static bool Reset(string query)
        {
            var resetPhrases = new List<string>()
            {
                "restart",
                "/start",
                "новый вопрос",
                "сброс",
                "заново",
                "reset",
                "новый вопрос",
                "еще вопрос",
                "еще один вопрос",
                "перезапуск",
                "перезапустить",
                "отмена",
                "отменить",
                "обновить",
                "привет",
                "здравствуйте",
                "сбросить"
            };

            return resetPhrases.Contains(query.ToLower());
        }
    }
}