using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList
{
    public class GetNotificationsPagedListQueryValidator : AbstractValidator<GetNotificationsPagedListQuery>
    {
        public GetNotificationsPagedListQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
