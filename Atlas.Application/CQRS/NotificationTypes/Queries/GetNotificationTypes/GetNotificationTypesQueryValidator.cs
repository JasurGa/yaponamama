using System;
using FluentValidation;

namespace Atlas.Application.CQRS.NotificationTypes.Queries.GetNotificationTypes
{
    public class GetNotificationTypesQueryValidator : AbstractValidator<GetNotificationTypesQuery>
    {
        public GetNotificationTypesQueryValidator()
        {
        }
    }
}
