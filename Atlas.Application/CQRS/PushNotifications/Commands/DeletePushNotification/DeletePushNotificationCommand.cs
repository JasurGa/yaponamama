using System;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Commands.DeletePushNotification
{
    public class DeletePushNotificationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

