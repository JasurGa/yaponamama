using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommandValidator
        : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

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
