using FluentValidation;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.FindStoreToGoodPagedList
{
    public class FindStoreToGoodPagedLIstQueryValidator : AbstractValidator<FindStoreToGoodPagedListQuery>
    {
        public FindStoreToGoodPagedLIstQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
