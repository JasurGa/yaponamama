using FluentValidation;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.FindDisposeToConsignmentPagedList
{
    public class FindDisposeToConsignmentPagedListQueryValidator : AbstractValidator<FindDisposeToConsignmentPagedListQuery>
    {
        public FindDisposeToConsignmentPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}
