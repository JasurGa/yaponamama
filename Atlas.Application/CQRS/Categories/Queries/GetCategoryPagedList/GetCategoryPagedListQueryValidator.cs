using System;
using FluentValidation;
namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList
{
    public class GetCategoryPagedListQueryValidator : AbstractValidator<GetCategoryPagedListQuery>
    {
        public GetCategoryPagedListQueryValidator()
        {
            RuleFor(e => e.ShowDeleted)
                .NotEmpty();

            RuleFor(e => e.PageSize)
               .NotEmpty();
        }
    }
}
