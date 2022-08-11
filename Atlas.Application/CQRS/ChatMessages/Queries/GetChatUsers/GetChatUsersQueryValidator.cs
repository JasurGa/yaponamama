using System;
using FluentValidation;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers
{
    public class GetChatUsersQueryValidator : AbstractValidator<GetChatUsersQuery>
    {
        public GetChatUsersQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
