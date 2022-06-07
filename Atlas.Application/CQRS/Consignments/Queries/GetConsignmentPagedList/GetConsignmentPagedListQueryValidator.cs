using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentPagedList
{
    public class GetConsignmentPagedListQueryValidator : AbstractValidator<GetConsignmentPagedListQuery>
    {
        public GetConsignmentPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
