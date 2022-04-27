using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListByStore
{
    public class GetVehiclePagedListByStoreQueryHandler : IRequestHandler<GetVehiclePagedListByStoreQuery, PageDto<VehicleLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVehiclePagedListByStoreQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<VehicleLookupDto>> Handle(GetVehiclePagedListByStoreQuery request, CancellationToken cancellationToken)
        {
            var vehiclesCount = await _dbContext.Vehicles.CountAsync();

            var vehicles = await _dbContext.Vehicles
                .Where(v => v.StoreId == request.StoreId)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<VehicleLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PageDto<VehicleLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = vehiclesCount,
                PageCount  = (int)Math.Ceiling((double)vehiclesCount / request.PageSize),
                Data       = vehicles,
            };
        }
    }
}
