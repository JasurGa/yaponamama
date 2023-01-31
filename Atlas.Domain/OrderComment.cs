using System;

namespace Atlas.Domain
{
    public class OrderComment
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order Order { get; set; }

        public User User { get; set; }
    }
}

