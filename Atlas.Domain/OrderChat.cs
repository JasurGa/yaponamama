using System;
namespace Atlas.Domain
{
    public class OrderChat
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime SendAt { get; set; }

        public Guid SentFromUserId { get; set; }
    }
}
