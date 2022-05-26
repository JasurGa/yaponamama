using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentDetails
{
    public class GetConsignmentDetailsQueryValidator : AbstractValidator<GetConsignmentDetailsQuery>
    {
        public GetConsignmentDetailsQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
