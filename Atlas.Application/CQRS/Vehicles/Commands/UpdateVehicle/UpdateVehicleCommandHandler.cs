using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateVehicleCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => 
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(x => 
                x.Id == request.VehicleTypeId, cancellationToken);

            if (vehicleType == null)
            {
                throw new NotFoundException(nameof(VehicleType), request.VehicleTypeId);
            }

            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new NotFoundException(nameof(Vehicle), request.Id);
            }

            vehicle.Name                             = request.Name;
            vehicle.RegistrationCertificatePhotoPath = request.RegistrationCertificatePhotoPath;
            vehicle.RegistrationNumber               = request.RegistrationNumber;
            vehicle.StoreId                          = request.StoreId;
            vehicle.VehicleTypeId                    = request.VehicleTypeId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
