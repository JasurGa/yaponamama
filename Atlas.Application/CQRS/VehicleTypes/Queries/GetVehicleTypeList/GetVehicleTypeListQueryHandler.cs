﻿using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.VehicleTypes.Queries.GetVehicleTypeList
{
    public class GetVehicleTypeListQueryHandler : IRequestHandler<GetVehicleTypeListQuery, VehicleTypeListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetVehicleTypeListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<VehicleTypeListVm> Handle(GetVehicleTypeListQuery request, CancellationToken cancellationToken)
        {
            var vehicleTypes = await _dbContext.VehicleTypes
                .ProjectTo<VehicleTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new VehicleTypeListVm { VehicleTypes = vehicleTypes };
        }
    }
}
