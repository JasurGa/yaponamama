using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetDiscountedGoodListByCategory
{
    public class GetDiscountedGoodListByCategoryQueryValidator : AbstractValidator<GetDiscountedGoodListByCategoryQuery>
    {
        public GetDiscountedGoodListByCategoryQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
