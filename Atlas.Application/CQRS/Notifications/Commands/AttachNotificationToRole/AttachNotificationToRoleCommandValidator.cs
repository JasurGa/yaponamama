using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToRole
{
    public class AttachNotificationToRoleCommandValidator :
        AbstractValidator<AttachNotificationToRoleCommand>
    {
        public AttachNotificationToRoleCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Role)
                .NotEmpty();
        }
    }
}
