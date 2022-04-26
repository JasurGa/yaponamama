using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails
{
    public class GetVehicleDetailsQuery : IRequest<VehicleDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
