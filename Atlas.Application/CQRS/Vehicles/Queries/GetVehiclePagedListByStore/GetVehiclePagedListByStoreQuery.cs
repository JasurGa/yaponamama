using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListByStore
{
    public class GetVehiclePagedListByStoreQuery : IRequest<PageDto<VehicleLookupDto>>
    {
        public Guid StoreId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
