using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTGroupOfficeSupportBot.Dialogs
{
    /// <summary>
    /// Класс служит помощником в определении недостающих параметров с последующей подсказкой пользователю
    /// </summary>
    public class ParametrsDialog
    {
        /// <summary>
        /// Метод определяет, какие параметры у нас заполнены, а какие нет
        /// </summary>
        /// <returns></returns>
        public static string CheckParametrs(string organization)
        {
            // Не заполнены поля "Организация"
            if (string.IsNullOrEmpty(organization))
            {
                return "Укажите, пожалуйста, какая организация Вас интересует?";
            }
            return "Что-то пошло не так, попробуйте еще раз";
        }
    }
}