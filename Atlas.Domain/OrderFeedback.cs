using System;

namespace Atlas.Domain
{
    public class OrderFeedback
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Rating { get; set; }

        public string Text { get; set; }
    }
}
