using System;
namespace Atlas.Identity.Models
{
    public class AuthToken
    {
        public string TokenType { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string RefreshToken { get; set; } 
    }
}
