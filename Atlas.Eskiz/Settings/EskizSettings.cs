using System;
namespace Atlas.Eskiz.Settings
{
    public class EskizSettings
    {
        public const string EskizSection = "Eskiz";

        public string Email { get; set; }

        public string Password { get; set; }

        public string FromNumber { get; set; }
    }
}

