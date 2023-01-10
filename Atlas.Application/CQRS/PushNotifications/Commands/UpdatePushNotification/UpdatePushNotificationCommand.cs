using System;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Commands.UpdatePushNotification
{
    public class UpdatePushNotificationCommand : IRequest
    {
        public Guid Id { get; set; }

        public string HeaderRu { get; set; }

        public string HeaderEn { get; set; }

        public string HeaderUz { get; set; }

        public string BodyRu { get; set; }

        public string BodyEn { get; set; }

        public string BodyUz { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}

