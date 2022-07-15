using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Atlas.Domain;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCourier
{
    public class UpdateCourierCommandHandler : IRequestHandler<UpdateCourierCommand>
    {
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public UpdateCourierCommandHandler(IMediator mediator, IAtlasDbContext dbContext) => 
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Unit> Handle(UpdateCourierCommand request,
            CancellationToken cancellationToken)
        {
            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if(courier == null)
            {
                throw new NotFoundException(nameof(Courier), request.Id);
            }

            request.User.Id = courier.UserId;
            await _mediator.Send(request.User);

            if (request.VehicleId != null)
            {
                var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x =>
                    x.Id == request.VehicleId, cancellationToken);

                if (vehicle == null)
                {
                    throw new NotFoundException(nameof(Vehicle), request.VehicleId);
                }
            }

            courier.UserId            = request.User.Id;
            courier.PhoneNumber       = request.PhoneNumber;
            courier.PassportPhotoPath = request.PassportPhotoPath;
            courier.DriverLicensePath = request.DriverLicensePath;
            courier.VehicleId         = request.VehicleId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
