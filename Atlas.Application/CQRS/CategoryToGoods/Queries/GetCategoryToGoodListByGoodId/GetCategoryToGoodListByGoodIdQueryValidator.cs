using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class GetCategoryToGoodListByGoodIdQueryValidator : AbstractValidator<GetCategoryToGoodListByGoodIdQuery>
    {
        public GetCategoryToGoodListByGoodIdQueryValidator()
        {
            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);
        }
    }
}
