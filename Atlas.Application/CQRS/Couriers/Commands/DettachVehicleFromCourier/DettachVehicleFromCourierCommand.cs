using MediatR;
using System;

namespace Atlas.Application.CQRS.Couriers.Commands.DetachVehicleFromCourier
{
    public class DettachVehicleFromCourierCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
