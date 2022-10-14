using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Couriers.Commands.DetachVehicleFromCourier;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Couriers.Commands.DettachVehicleFromCourier
{
    public class DettachVehicleFromCourierCommandHandler : IRequestHandler<DettachVehicleFromCourierCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DettachVehicleFromCourierCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DettachVehicleFromCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (courier == null)
            {
                throw new NotFoundException(nameof(Courier), request.Id);
            }

            courier.VehicleId = null;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
