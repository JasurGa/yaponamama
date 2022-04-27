using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleListByStore
{
    public class GetVehicleListByStoreValidator : AbstractValidator<GetVehicleListByStoreQuery>
    {
        public GetVehicleListByStoreValidator()
        {
            RuleFor(v => v.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
