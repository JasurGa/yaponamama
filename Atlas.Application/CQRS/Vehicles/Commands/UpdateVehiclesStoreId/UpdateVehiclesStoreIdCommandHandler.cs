using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Vehicles.Commands.UpdateVehiclesStoreId
{
    public class UpdateVehiclesStoreIdCommandHandler :
        IRequestHandler<UpdateVehiclesStoreIdCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateVehiclesStoreIdCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateVehiclesStoreIdCommand request,
            CancellationToken cancellationToken)
        {
            var storeId = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (storeId == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var vehicles = await _dbContext.Vehicles.Where(x =>
                request.VehicleIds.Contains(x.Id))
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
