using System;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetUnreadPushNotifications
{
    public class GetUnreadPushNotificationsQuery : IRequest<PushNotificationListVm>
    {
        public Guid ClientId { get; set; }
    }
}

