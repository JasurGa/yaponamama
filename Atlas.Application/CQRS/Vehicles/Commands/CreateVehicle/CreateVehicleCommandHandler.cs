﻿using Atlas.Application.Common.Exceptions;
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

namespace Atlas.Application.CQRS.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateVehicleCommandHandler(IAtlasDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var store       = await _dbContext.Stores.FirstOrDefaultAsync(s => 
                s.Id == request.StoreId, cancellationToken);

            var vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(vt =>
                vt.Id == request.VehicleTypeId, cancellationToken);


            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            if (vehicleType == null)
            {
                throw new NotFoundException(nameof(VehicleType), request.VehicleTypeId);
            }


            var vehicle = new Vehicle
            {
                Id                               = Guid.NewGuid(),
                Name                             = request.Name,
                RegistrationCertificatePhotoPath = request.RegistrationCertificatePhotoPath,
                RegistrationNumber               = request.RegistrationNumber,
                VehicleTypeId                    = request.VehicleTypeId,
                StoreId                          = request.StoreId,
            };

            await _dbContext.Vehicles.AddAsync(vehicle, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return vehicle.Id;
        }

    }
}