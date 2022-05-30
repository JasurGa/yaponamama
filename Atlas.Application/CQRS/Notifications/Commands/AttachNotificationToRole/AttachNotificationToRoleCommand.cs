using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToRole
{
    public class AttachNotificationToRoleCommand : IRequest
    {
        public Guid NotificationId { get; set; }

        public string Role { get; set; }
    }
}
