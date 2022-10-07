using System;

namespace Atlas.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string PaycomId { get; set; }

        public ulong PaycomTime { get; set; }

        public int PaycomAmount { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime PerformedAt { get; set; }

        public DateTime CanceledAt { get; set; }

        public int State { get; set; }

        public int? Reason { get; set; }
    }
}

