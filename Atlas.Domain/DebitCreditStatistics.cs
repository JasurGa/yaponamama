using System;
namespace Atlas.Domain
{
    public class DebitCreditStatistics
    {
        public Guid Id { get; set; }

        public long Debit { get; set; }

        public long Credit { get; set; }

        public DateTime AddedAt { get; set; }
    }
}

