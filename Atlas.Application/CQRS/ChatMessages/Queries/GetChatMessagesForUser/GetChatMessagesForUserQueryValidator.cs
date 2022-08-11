using System;
using FluentValidation;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatMessagesForUser
{
    public class GetChatMessagesForUserQueryValidator : AbstractValidator<GetChatMessagesForUserQuery>
    {
        public GetChatMessagesForUserQueryValidator()
        {
            RuleFor(x => x.MyUserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ChatUserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
