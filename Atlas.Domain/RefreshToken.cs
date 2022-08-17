using System;
namespace Atlas.Domain
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Refresh { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
