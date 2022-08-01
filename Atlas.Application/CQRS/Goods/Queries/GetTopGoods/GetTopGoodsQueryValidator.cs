using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetTopGoods
{
    public class GetTopGoodsQueryValidator :
        AbstractValidator<GetTopGoodsQuery>
    {
        public GetTopGoodsQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
