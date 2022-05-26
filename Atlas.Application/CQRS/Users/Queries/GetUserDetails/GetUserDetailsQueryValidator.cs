using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryValidator : AbstractValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
