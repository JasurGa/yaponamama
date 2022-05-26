using FluentValidation;
namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList
{
    public class GetCategoryPagedListQueryValidator : AbstractValidator<GetCategoryPagedListQuery>
    {
        public GetCategoryPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
               .NotEmpty();
        }
    }
}
