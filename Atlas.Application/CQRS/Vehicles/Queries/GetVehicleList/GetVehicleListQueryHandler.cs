using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList
{
    public class GetVehicleListQueryHandler : IRequestHandler<GetVehicleListQuery, VehicleListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVehicleListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VehicleListVm> Handle(GetVehicleListQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _dbContext.Vehicles
                .ProjectTo<VehicleLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new VehicleListVm { Vehicles = vehicles};
        }
    }
}
