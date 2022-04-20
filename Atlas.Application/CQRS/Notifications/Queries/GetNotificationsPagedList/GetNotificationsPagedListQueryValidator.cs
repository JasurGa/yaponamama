using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList
{
    public class GetNotificationsPagedListQueryValidator : AbstractValidator<GetNotificationsPagedListQuery>
    {
        public GetNotificationsPagedListQueryValidator()
        {
            RuleFor(e => e.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PageSize)
                .NotEmpty();
        }
    }
}
