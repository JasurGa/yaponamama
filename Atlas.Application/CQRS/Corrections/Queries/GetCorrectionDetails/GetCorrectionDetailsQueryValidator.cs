using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionDetails
{
    public class GetCorrectionDetailsQueryValidator : AbstractValidator<GetCorrectionDetailsQuery>
    {
        public GetCorrectionDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
