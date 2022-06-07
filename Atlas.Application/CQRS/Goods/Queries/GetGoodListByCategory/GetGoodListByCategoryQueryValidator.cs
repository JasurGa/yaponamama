using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GetGoodListByCategoryQueryValidator : AbstractValidator<GetGoodListByCategoryQuery>
    {
        public GetGoodListByCategoryQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
