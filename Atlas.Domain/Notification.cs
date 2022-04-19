using System;
namespace Atlas.Domain
{
    public class Notification
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Priority { get; set; }
    }
}
