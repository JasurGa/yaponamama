using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleListByStore
{
    public class GetVehicleListByStoreQueryHandler : IRequestHandler<GetVehicleListByStoreQuery, VehicleListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVehicleListByStoreQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VehicleListVm> Handle(GetVehicleListByStoreQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _dbContext.Vehicles
                .Where(x => x.StoreId == request.StoreId)
                .ProjectTo<VehicleLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return new VehicleListVm { Vehicles = vehicles };
        }
    }
}
