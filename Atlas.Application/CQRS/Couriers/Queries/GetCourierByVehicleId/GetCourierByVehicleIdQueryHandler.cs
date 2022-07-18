using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierByVehicleId
{
    public class GetCourierByVehicleIdQueryHandler : IRequestHandler<GetCourierByVehicleIdQuery,
        List<CourierDetailsVm>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCourierByVehicleIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<List<CourierDetailsVm>> Handle(GetCourierByVehicleIdQuery request,
            CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x =>
                x.Id == request.VehicleId, cancellationToken);

            if (vehicle == null)
            {
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);
            }

            var couriers = await _dbContext.Couriers.Include(x => x.User)
                .Where(x => x.VehicleId == request.VehicleId &&
                    x.IsDeleted == request.ShowDeleted)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<Courier>, List<CourierDetailsVm>>(couriers);
        }
    }
}
