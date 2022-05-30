using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToUser
{
    public class AttachNotificationToUserCommandValidator
        : AbstractValidator<AttachNotificationToUserCommand>
    {
        public AttachNotificationToUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.NotificationId)
                .NotEqual(Guid.Empty);
        }
    }
}
