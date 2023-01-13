using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PushNotifications.Commands.DeletePushNotification
{
    public class DeletePushNotificationCommandValidator : AbstractValidator<DeletePushNotificationCommand>
    {
        public DeletePushNotificationCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

