﻿using MediatR;
using System;

namespace Atlas.Application.CQRS.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string RegistrationCertificatePhotoPath { get; set; }

        public string RegistrationNumber { get; set; }

        public string RegistrationCertificateNumber { get; set; }

        public Guid VehicleTypeId { get; set; }

        public Guid StoreId { get; set; }
    }
}
