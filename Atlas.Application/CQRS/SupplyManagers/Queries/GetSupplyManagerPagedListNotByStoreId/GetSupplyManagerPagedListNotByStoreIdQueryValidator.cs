using System;
using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedListNotByStoreId
{
    public class GetSupplyManagerPagedListNotByStoreIdQueryValidator :
        AbstractValidator<GetSupplyManagerPagedListNotByStoreIdQuery>
    {
        public GetSupplyManagerPagedListNotByStoreIdQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
