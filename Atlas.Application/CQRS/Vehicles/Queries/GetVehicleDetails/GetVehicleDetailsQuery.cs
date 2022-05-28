using System;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails
{
    public class GetVehicleDetailsQuery : IRequest<VehicleDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
