using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory
{
    public class GetGoodPagedListByCategoryQueryValidator : AbstractValidator<GetGoodPagedListByCategoryQuery>
    {
        public GetGoodPagedListByCategoryQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
