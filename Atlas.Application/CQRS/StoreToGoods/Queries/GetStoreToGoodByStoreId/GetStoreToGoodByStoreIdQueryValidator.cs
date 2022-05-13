using System;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class GetStoreToGoodByStoreIdQueryValidator : AbstractValidator<GetStoreToGoodByStoreIdQuery>
    {
        public GetStoreToGoodByStoreIdQueryValidator()
        {
            RuleFor(stg => stg.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
