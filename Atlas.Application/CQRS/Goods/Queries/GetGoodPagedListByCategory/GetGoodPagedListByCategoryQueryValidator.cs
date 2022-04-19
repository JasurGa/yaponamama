using System;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory
{
    public class GetGoodPagedListByCategoryQueryValidator : AbstractValidator<GetGoodPagedListByCategoryQuery>
    {
        public GetGoodPagedListByCategoryQueryValidator()
        {
            RuleFor(e => e.PageSize)
                .NotEmpty();

            RuleFor(e => e.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
