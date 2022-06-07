using MediatR;
using System;

namespace Atlas.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
