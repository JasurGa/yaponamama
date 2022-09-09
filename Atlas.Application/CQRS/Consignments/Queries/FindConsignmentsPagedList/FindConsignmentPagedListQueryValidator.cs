using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList
{
    public class FindConsignmentPagedListQueryValidator : AbstractValidator<FindConsignmentPagedListQuery>
    {
        public FindConsignmentPagedListQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
                .NotNull();

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

