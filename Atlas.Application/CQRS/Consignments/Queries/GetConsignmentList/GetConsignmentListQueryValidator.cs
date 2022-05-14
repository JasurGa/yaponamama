using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class GetConsignmentListQueryValidator : AbstractValidator<GetConsignmentListQuery>
    {
        public GetConsignmentListQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
