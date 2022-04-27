using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleListByStore
{
    public class GetVehicleListByStoreQuery : IRequest<VehicleListVm>
    {
        public Guid StoreId { get; set; }
    }
}
