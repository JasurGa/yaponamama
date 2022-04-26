using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedList
{
    public class GetVehiclePagedListQueryValidator : AbstractValidator<GetVehiclePagedListQuery>
    {
        public GetVehiclePagedListQueryValidator()
        {
            RuleFor(o => o.PageSize)
                .NotEmpty();
        }
    }
}
