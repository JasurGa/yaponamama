using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportDetails
{
    public class GetSupportDetailsQueryValidator : AbstractValidator<GetSupportDetailsQuery>
    {
        public GetSupportDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
