using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByPromoCategory
{
    public class GetGoodPagedListByPromoCategoryQueryValidator : AbstractValidator<GetGoodPagedListByPromoCategoryQuery>
    {
        public GetGoodPagedListByPromoCategoryQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(e => e.PageIndex)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PromoCategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}

