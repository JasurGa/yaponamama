using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Commands.CreateCourier
{
    public class CreateCourierCommandHandler : IRequestHandler<CreateCourierCommand, Guid>
    {
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public CreateCourierCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            if (request.VehicleId != null)
            {
                var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x =>
                    x.Id == request.VehicleId, cancellationToken);

                if (vehicle == null)
                {
                    throw new NotFoundException(nameof(Vehicle), request.VehicleId);
                }
            }

            var userId = await _mediator.Send(request.User, cancellationToken);

            var courier = new Courier
            {
                Id                = Guid.NewGuid(),
                UserId            = userId,
                PhoneNumber       = request.PhoneNumber,
                PassportPhotoPath = request.PassportPhotoPath,
                DriverLicensePath = request.DriverLicensePath,
                VehicleId         = request.VehicleId,
                Balance           = 0,
                KPI               = 0, 
                IsDeleted         = false,
            };

            await _dbContext.Couriers.AddAsync(courier, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return courier.Id;
        }
    }
}
