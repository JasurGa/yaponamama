using System;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationById
{
    public class GetPushNotificationByIdQuery : IRequest<PushNotificationLookupDto>
    {
        public Guid Id { get; set; }
    }
}

