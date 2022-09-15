using System;

namespace Atlas.Domain
{
    public class ChatMessage
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public int MessageType { get; set; }

        public string Body { get; set; }

        public string Optional { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool HasBeenRead { get; set; }
    }
}
