using System.Collections.Generic;
using Newtonsoft.Json;

namespace QnA
{
    /// <summary>
    /// Класс для разбивания Json
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Вытаскивание Json PossibleAnswer
        /// </summary>
        [JsonProperty("answer")]
        public string PossibleAnswer { get; set; }

        /// <summary>
        /// Вытаскивание Json score
        /// </summary>
        public string Score { get; set; }

        public List<string> Questions { get; set; }
    }
}
