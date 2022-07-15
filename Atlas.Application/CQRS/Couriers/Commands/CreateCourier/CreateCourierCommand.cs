using Atlas.Application.CQRS.Users.Commands.CreateUser;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Couriers.Commands.CreateCourier
{
    public class CreateCourierCommand : IRequest<Guid>
    {
        public CreateUserCommand User { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public Guid? VehicleId { get; set; }
    }
}
