using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }
    }
}
