using FluentValidation;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList
{
    public class GetDisposeToConsignmentPagedListQueryValidator : AbstractValidator<GetDisposeToConsignmentPagedListQuery>
    {
        public GetDisposeToConsignmentPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
