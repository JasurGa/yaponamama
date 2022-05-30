using System;
namespace Atlas.Domain
{
    public class NotificationAccess
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Notification Notification { get; set; }

        public Guid NotificationId { get; set; }
    }
}
