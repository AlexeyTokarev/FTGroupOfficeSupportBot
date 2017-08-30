using System.Collections.Generic;

namespace ApiAi
{
    public class ApiAiResult
    {
        public string Organization { get; set; }
        
        public ICollection<string> Errors { get; set; }

        public ApiAiResult()
        {
            Errors = new List<string>();
        }
    }
}
