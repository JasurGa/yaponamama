using FluentValidation;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedListByStoreId
{
    public class GetSupplyManagerPagedListByStoreIdQueryValidator : AbstractValidator<GetSupplyManagerPagedListByStoreIdQuery>
    {
        public GetSupplyManagerPagedListByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
