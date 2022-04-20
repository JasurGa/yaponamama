using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientDetails
{
    public class GetClientDetailsQueryValidator : AbstractValidator<GetClientDetailsQuery>
    {
        public GetClientDetailsQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
