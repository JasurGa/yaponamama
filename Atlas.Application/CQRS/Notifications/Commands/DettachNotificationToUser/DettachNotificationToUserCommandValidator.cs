using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToUser
{
    public class DettachNotificationToUserCommandValidator
        : AbstractValidator<DettachNotificationToUserCommand>
    {
        public DettachNotificationToUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.NotificationId)
                .NotEqual(Guid.Empty);
        }
    }
}
