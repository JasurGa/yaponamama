using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedList
{
    public class GetVehiclePagedListQuery : IRequest<PageDto<VehicleLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
