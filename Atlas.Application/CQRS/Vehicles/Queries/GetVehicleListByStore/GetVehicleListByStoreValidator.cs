using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleListByStore
{
    public class GetVehicleListByStoreValidator : AbstractValidator<GetVehicleListByStoreQuery>
    {
        public GetVehicleListByStoreValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
