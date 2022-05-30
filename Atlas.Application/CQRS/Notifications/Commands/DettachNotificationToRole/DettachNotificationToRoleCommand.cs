using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToRole
{
    public class DettachNotificationToRoleCommand : IRequest
    {
        public string Role { get; set; }

        public Guid NotificationId { get; set; }
    }
}
