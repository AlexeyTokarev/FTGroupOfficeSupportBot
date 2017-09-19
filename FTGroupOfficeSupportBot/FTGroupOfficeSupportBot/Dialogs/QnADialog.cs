using QnA;
using System.Web;

namespace FTGroupOfficeSupportBot.Dialogs
{
    public class QnADialog
    {
        private string _knowledgebaseId; // Идентификатор базы знаний для бота QnA Maker
        private string _qnamakerSubscriptionKey; // Использование ключа подписи в QnA Maker

        /// <summary>
        /// Данный метод назначает необходимые ключи для работы с ботом QnA Maker
        /// </summary>
        /// <param name="organization"></param>
        public void QnAMakerKey(string organization)
        {
            switch (organization)
            {
                case "Финтендер":
                    {
                        _knowledgebaseId = "58a4b585-c092-40a9-a7d1-0fe441766581";
                        _qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d";
                        break;
                    }
                case "РТС-Тендер":
                    {
                        _knowledgebaseId = "a61b7579-7eec-44d4-8c84-fcc38b1b6f38";
                        _qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d";
                        break;
                    }
                case "РТСТ":
                    {
                        _knowledgebaseId = "fb650d73-08aa-4c31-98d4-d682d50c99ce";
                        _qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d";
                        break;
                    }
                case "Венчур Lab":
                    {
                        _knowledgebaseId = "d9a3be38-fc94-4524-8639-a032d7bf8582";
                        _qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d";
                        break;
                    }
                case "Банк СКИБ":
                    {
                        _knowledgebaseId = "c3e0f0cd-3391-4362-b3d7-12e43df79ff3";
                        _qnamakerSubscriptionKey = "850a8ac4def146498ab7e2161cd87c9d";
                        break;
                    }
            }
        }

        /// <summary>
        /// Данныый метод посылает запросы на бота QnA Maker и получает ответы на эти запросы
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="qnaResponse"></param>
        /// <returns></returns>
        public string QnABotResponse(string platform, string qnaResponse)
        {
            QnAMakerKey(platform);

            string qnaResult = QnARequest.QnAResponse(_knowledgebaseId, _qnamakerSubscriptionKey, qnaResponse);

            return HttpUtility.HtmlDecode(qnaResult);
        }
    }
}