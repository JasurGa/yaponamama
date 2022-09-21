using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteVehicleCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteVehicleCommand request,
            CancellationToken cancellationToken)
        {
            var vehicle = await _dbContext.Vehicles.Include(x => x.Courier)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new NotFoundException(nameof(Vehicle), request.Id);
            }

            vehicle.Courier.VehicleId = null;
            await _dbContext.SaveChangesAsync(cancellationToken);

            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
