using FluentValidation;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId
{
    public class GetStoreToGoodPagedListByStoreIdQueryValidator : AbstractValidator<GetStoreToGoodPagedListByStoreIdQuery>
    {
        public GetStoreToGoodPagedListByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
