using FluentValidation;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentDetails
{
    public class GetDisposeToConsignmentDetailsQueryValidator : AbstractValidator<GetDisposeToConsignmentDetailsQuery>
    {
        public GetDisposeToConsignmentDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
