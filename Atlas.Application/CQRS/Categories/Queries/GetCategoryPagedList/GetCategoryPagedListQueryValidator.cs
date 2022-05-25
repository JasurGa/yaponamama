using FluentValidation;
namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList
{
    public class GetCategoryPagedListQueryValidator : AbstractValidator<GetCategoryPagedListQuery>
    {
        public GetCategoryPagedListQueryValidator()
        {
            RuleFor(e => e.ShowDeleted)
                .Must(sd => sd == true || sd == false);

            RuleFor(e => e.PageSize)
               .NotEmpty();
        }
    }
}
