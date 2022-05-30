using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(x => x.NotificationTypeId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Subject)
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotEmpty();
        }
    }
}
