using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class GetCategoryAndGoodListByMainCategoryQueryValidator : AbstractValidator<GetCategoryAndGoodListByMainCategoryQuery>
    {
        public GetCategoryAndGoodListByMainCategoryQueryValidator()
        {
            RuleFor(x => x.MainCategoryId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodListSize)
                .NotEmpty()
                .LessThanOrEqualTo(1000);
        }
    }
}
