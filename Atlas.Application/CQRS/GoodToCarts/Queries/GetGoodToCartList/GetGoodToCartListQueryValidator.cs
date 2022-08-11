using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList
{
    public class GetGoodToCartListQueryValidator : AbstractValidator<GetGoodToCartListQuery>
    {
        public GetGoodToCartListQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
