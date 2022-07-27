using Atlas.Application.Common.Exceptions;
using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehiclePagedListNotByStore
{
    public class GetVehiclePagedListNotByStoreQueryHandler : IRequestHandler<GetVehiclePagedListNotByStoreQuery,
        PageDto<VehicleLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVehiclePagedListNotByStoreQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<VehicleLookupDto>> Handle(GetVehiclePagedListNotByStoreQuery request,
            CancellationToken cancellationToken)
        {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var vehiclesCount = await _dbContext.Vehicles.CountAsync(x =>
                x.StoreId != request.StoreId &&
                x.IsDeleted == request.ShowDeleted,
                cancellationToken);

            var vehicles = await _dbContext.Vehicles
                .Where(x => x.StoreId != request.StoreId &&
                    x.IsDeleted == request.ShowDeleted)
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<VehicleLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

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
