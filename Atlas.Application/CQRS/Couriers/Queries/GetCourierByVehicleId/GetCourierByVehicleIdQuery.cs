using System;
using System.Collections.Generic;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierByVehicleId
{
    public class GetCourierByVehicleIdQuery : IRequest<List<CourierDetailsVm>>
    {
        public Guid VehicleId { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
