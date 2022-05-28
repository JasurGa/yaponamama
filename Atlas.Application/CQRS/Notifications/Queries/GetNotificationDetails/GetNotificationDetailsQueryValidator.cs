using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails
{
    public class GetNotificationDetailsQueryValidator : AbstractValidator<GetNotificationDetailsQuery>
    {
        public GetNotificationDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
