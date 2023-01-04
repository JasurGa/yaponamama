using System;
namespace Atlas.Domain
{
    public class PromoUsage
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid PromoId { get; set; }

        public DateTime UsedAt { get; set; }
    }
}

