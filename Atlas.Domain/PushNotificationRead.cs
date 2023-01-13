using System;
namespace Atlas.Domain
{
    public class PushNotificationRead
    {
        public Guid Id { get; set; }

        public Guid NotificationId { get; set; }

        public Guid ClientId { get; set; }

        public DateTime SendAt { get; set; }
    }
}

