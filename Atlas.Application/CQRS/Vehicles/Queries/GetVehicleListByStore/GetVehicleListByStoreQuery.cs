using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleListByStore
{
    public class GetVehicleListByStoreQuery : IRequest<VehicleListVm>
    {
        public Guid StoreId { get; set; }
    }
}
