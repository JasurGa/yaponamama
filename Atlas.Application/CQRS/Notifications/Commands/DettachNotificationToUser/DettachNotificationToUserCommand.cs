using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToUser
{
    public class DettachNotificationToUserCommand : IRequest
    {
        public Guid UserId { get; set; }

        public Guid NotificationId { get; set; }
    }
}
