using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetailsByStore
{
    public class GetVehicleDetailsByStoreQuery : IRequest<VehicleDetailsVm>
    {
        public Guid StoreId { get; set; }
    }
}
