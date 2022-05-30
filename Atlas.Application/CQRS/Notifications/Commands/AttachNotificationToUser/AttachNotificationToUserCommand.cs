using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToUser
{
    public class AttachNotificationToUserCommand : IRequest
    {
        public Guid UserId { get; set; }

        public Guid NotificationId { get; set; }
    }
}
