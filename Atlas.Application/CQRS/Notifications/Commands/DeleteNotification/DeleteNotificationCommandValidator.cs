using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandValidator
        : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
