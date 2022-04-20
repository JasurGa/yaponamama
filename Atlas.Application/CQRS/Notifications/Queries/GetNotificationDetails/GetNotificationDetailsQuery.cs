using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails
{
    public class GetNotificationDetailsQuery : IRequest<NotificationDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
