using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToRole
{
    public class DettachNotificationToRoleCommandValidator
        : AbstractValidator<DettachNotificationToRoleCommand>
    {
        public DettachNotificationToRoleCommandValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty();

            RuleFor(x => x.NotificationId)
                .NotEqual(Guid.Empty);
        }
    }
}
