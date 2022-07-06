using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCouriersStoreId
{
    public class UpdateCouriersStoreIdCommandHandler :
        IRequestHandler<UpdateCouriersStoreIdCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateCouriersStoreIdCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCouriersStoreIdCommand request,
            CancellationToken cancellationToken)
        {
            var couriers = await _dbContext.Couriers.Where(x =>
                request.CourierIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var vehicles = await _dbContext.Vehicles.Where(x =>
                couriers.Select(x => x.VehicleId).Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var vehicle in vehicles)
            {
                vehicle.StoreId = request.StoreId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
