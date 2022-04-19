using System;
namespace Atlas.Domain
{
    public class SupportNote
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid SupportId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime SendAt { get; set; }

        public Guid SentFromUserId { get; set; }
    }
}
