using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListNotByStore
{
    public class GetVehiclePagedListNotByStoreQueryValidator :
        AbstractValidator<GetVehiclePagedListNotByStoreQuery>
    {
        public GetVehiclePagedListNotByStoreQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
