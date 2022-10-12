using System;
namespace Atlas.Eskiz.Models
{
    public class EskizAuthResponse
    {
        public string message { get; set; }

        public EskizToken data { get; set; }

        public string token_type { get; set; }
    }
}

