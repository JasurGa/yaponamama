using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Commands.UpdateVehiclesStoreId
{
    public class UpdateVehiclesStoreIdCommand : IRequest
    {
        public Guid StoreId { get; set; }

        public List<Guid> VehicleIds { get; set; }
    }
}
