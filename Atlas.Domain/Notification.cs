using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class Notification
    {
        public Guid Id { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public long Priority { get; set; }

        public List<NotificationAccess> NotificationAccesses { get; set; }
    }
}
