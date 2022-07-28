using FluentValidation;
using System;
namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByProvider
{
    public class GetGoodListByProviderQueryValidator : AbstractValidator<GetGoodListByProviderQuery>
    {
        public GetGoodListByProviderQueryValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);
        }
    }
}
