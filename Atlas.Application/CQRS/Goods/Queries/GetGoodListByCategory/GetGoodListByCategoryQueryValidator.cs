using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GetGoodListByCategoryQueryValidator : AbstractValidator<GetGoodListByCategoryQuery>
    {
        public GetGoodListByCategoryQueryValidator()
        {
            RuleFor(e => e.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
