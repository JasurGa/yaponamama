using System.Collections.Generic;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetUnreadPushNotifications
{
    public class PushNotificationListVm
    {
        public ICollection<PushNotificationLookupDto> PushNotifications { get; set; }
    }
}