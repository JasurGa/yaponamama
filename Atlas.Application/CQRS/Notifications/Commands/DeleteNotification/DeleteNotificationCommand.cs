using System;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
