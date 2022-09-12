using System;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.FindVehiclesPagedList
{
    public class FindVehiclesPagedListQuery : IRequest<PageDto<VehicleLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

