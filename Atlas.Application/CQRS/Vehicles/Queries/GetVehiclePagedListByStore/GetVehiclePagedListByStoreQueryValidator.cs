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
            RuleFor(v => v.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(v => v.PageSize)
                .NotEmpty();
        }
    }
}
