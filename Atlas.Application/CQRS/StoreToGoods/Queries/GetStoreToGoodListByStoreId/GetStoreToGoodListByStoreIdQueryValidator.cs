using System;
using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodListByStoreId;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class GetStoreToGoodListByStoreIdQueryValidator : AbstractValidator<GetStoreToGoodListByStoreIdQuery>
    {
        public GetStoreToGoodListByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
