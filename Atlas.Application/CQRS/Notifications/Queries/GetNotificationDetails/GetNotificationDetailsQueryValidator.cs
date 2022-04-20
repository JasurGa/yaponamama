using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails
{
    public class GetNotificationDetailsQueryValidator : AbstractValidator<GetNotificationDetailsQuery>
    {
        public GetNotificationDetailsQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
