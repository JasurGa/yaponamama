using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByProvider
{
    public class GetGoodPagedListByProviderQueryValidator : AbstractValidator<GetGoodPagedListByProviderQuery>
    {
        public GetGoodPagedListByProviderQueryValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
