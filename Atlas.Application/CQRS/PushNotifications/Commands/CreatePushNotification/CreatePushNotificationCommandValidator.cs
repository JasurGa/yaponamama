using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PushNotifications.Commands.CreatePushNotification
{
    public class CreatePushNotificationCommandValidator : AbstractValidator<CreatePushNotificationCommand>
    {
        public CreatePushNotificationCommandValidator()
        {
        }
    }
}

