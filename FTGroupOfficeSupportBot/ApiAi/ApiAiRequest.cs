using ApiAiSDK;
using System;

namespace ApiAi
{
    public class ApiAiRequest
    {
        private const string ClientAccessToken = "19bdbf4ea418461da45ed3b1f0e85abb";

        public static ApiAiResult ApiAiBotRequest(string request)
        {
            var result = new ApiAiResult();

            if (String.IsNullOrWhiteSpace(request))
            {
                result.Errors.Add("Напишите, пожалуйста, Ваш запрос! Что-то пошло не так");
                return result;
            }

            // Конфигурация агента Api.ai
            var config = new AIConfiguration(ClientAccessToken, SupportedLanguage.Russian);
            var apiAi = new ApiAiSDK.ApiAi(config);

            // Ответ
            var response = apiAi.TextRequest(request);

            if (response == null || response.Result == null || response.Result.Parameters == null)
            {
                result.Errors.Add("Напишите, пожалуйста, Ваш запрос! Что-то пошло не так");
                return result;
            }
            
            if (response.Result.Parameters.ContainsKey("Organization"))
            {
                string organization = response.Result.Parameters["Organization"].ToString();

                if (organization == "РТС-Тендер" || organization == "Финтендер")
                {
                    result.Organization = organization;
                }
                else
                {
                    result.Organization = null;
                }
            }
            
            return result;
        }
    }
}
