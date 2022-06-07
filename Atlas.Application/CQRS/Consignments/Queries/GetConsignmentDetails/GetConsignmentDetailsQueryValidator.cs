using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentDetails
{
    public class GetConsignmentDetailsQueryValidator : AbstractValidator<GetConsignmentDetailsQuery>
    {
        public GetConsignmentDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
