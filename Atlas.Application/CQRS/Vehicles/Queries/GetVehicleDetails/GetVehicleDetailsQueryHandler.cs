using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails
{
    public class GetVehicleDetailsQueryHandler : IRequestHandler<GetVehicleDetailsQuery, VehicleDetailsVm>
    {
        readonly private IMapper _mapper;
        readonly private IAtlasDbContext _dbContext;

        public GetVehicleDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VehicleDetailsVm> Handle(GetVehicleDetailsQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new NotFoundException(nameof(Vehicle), request.Id);
            }

            return _mapper.Map<VehicleDetailsVm>(vehicle);
        }
    }
}
