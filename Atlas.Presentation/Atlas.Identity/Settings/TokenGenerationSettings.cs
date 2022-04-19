using System;
using Microsoft.IdentityModel.Tokens;

namespace Atlas.Identity.Settings
{
    public class TokenGenerationSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Secret { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}
