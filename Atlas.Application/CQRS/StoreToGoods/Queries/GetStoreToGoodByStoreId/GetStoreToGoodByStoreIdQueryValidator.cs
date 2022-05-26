using System;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class GetStoreToGoodByStoreIdQueryValidator : AbstractValidator<GetStoreToGoodByStoreIdQuery>
    {
        public GetStoreToGoodByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
