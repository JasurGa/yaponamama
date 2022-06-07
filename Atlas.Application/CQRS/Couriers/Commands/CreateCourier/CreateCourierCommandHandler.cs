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
        private readonly IAtlasDbContext _dbContext;

        public CreateCourierCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (request.VehicleId != null)
            {
                var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x =>
                    x.Id == request.VehicleId, cancellationToken);

                if (vehicle == null)
                {
                    throw new NotFoundException(nameof(Vehicle), request.VehicleId);
                }
            }

            var courier = new Courier
            {
                Id                = Guid.NewGuid(),
                UserId            = user.Id,
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
