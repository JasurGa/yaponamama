using System;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Commands.CreatePushNotification
{
    public class CreatePushNotificationCommand : IRequest<Guid>
    {
        public string HeaderRu { get; set; }

        public string HeaderEn { get; set; }

        public string HeaderUz { get; set; }

        public string BodyRu { get; set; }

        public string BodyEn { get; set; }

        public string BodyUz { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}

