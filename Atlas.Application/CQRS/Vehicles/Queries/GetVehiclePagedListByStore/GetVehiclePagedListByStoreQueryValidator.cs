using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListByStore
{
    public class GetVehiclePagedListByStoreQueryValidator : AbstractValidator<GetVehiclePagedListByStoreQuery>
    {
        public GetVehiclePagedListByStoreQueryValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
