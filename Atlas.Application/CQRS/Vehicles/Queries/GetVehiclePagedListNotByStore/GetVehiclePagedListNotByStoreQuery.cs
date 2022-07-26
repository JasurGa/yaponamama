using System;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListNotByStore
{
    public class GetVehiclePagedListNotByStoreQuery : IRequest<PageDto<VehicleLookupDto>>
    {
        public Guid StoreId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public bool ShowDeleted { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }
    }
}
