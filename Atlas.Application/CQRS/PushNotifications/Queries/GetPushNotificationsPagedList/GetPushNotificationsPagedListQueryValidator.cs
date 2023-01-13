using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList
{
    public class GetPushNotificationsPagedListQueryValidator : AbstractValidator<GetPushNotificationsPagedListQuery>
    {
        public GetPushNotificationsPagedListQueryValidator()
        {
            RuleFor(e => e.PageSize)
                .GreaterThan(0);

            RuleFor(e => e.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

