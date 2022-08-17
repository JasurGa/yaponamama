using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.NotificationTypes.Queries.GetNotificationTypes
{
    public class NotificationTypeListVm
    {
        public List<NotificationTypeLookupDto> Notifications { get; set; }
    }
}
