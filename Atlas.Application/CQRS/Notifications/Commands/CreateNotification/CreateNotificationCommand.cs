using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }
    }
}
