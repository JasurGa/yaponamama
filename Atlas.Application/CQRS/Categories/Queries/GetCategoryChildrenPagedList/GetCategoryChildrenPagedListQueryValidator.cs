using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryChildrenPagedList
{
    public class GetCategoryChildrenPagedListQueryValidator : AbstractValidator<GetCategoryChildrenPagedListQuery>
    {
        public GetCategoryChildrenPagedListQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
