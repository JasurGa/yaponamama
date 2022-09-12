using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Vehicles.Queries.FindVehiclesPagedList
{
    public class FindVehiclesPagedListQueryHandler : IRequestHandler<FindVehiclesPagedListQuery,
        PageDto<VehicleLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindVehiclesPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<VehicleLookupDto>> Handle(FindVehiclesPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var vehicles = _dbContext.Vehicles.OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance($"{x.Name} {x.RegistrationNumber}",
                    request.SearchQuery));

            var vehiclesCount = await vehicles.CountAsync(cancellationToken);
            var pagedVehicles = await vehicles.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<VehicleLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = vehiclesCount,
                PageCount  = (int)Math.Ceiling((double)vehiclesCount / request.PageSize),
                Data       = _mapper.Map<List<Vehicle>, List<VehicleLookupDto>>(pagedVehicles),
            };
        }
    }
}

