using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetailsByStore
{
    public class GetVehicleDetailsByStoreQueryHandler : IRequestHandler<GetVehicleDetailsByStoreQuery, VehicleDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVehicleDetailsByStoreQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VehicleDetailsVm> Handle(GetVehicleDetailsByStoreQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.StoreId == request.StoreId, cancellationToken);

            if (vehicle == null)
            {
                throw new NotFoundException(nameof(Vehicle), request.StoreId);
            }

            return _mapper.Map<VehicleDetailsVm>(vehicle);
        }
    }
}
