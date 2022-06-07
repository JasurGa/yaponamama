using FluentValidation;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList
{
    public class GetSupportPagedListQueryValidator : AbstractValidator<GetSupportPagedListQuery>
    {
        public GetSupportPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
