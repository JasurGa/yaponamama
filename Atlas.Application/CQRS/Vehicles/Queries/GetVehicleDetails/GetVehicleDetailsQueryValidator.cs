using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails
{
    public class GetVehicleDetailsQueryValidator : AbstractValidator<GetVehicleDetailsQuery>
    {
        public GetVehicleDetailsQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
