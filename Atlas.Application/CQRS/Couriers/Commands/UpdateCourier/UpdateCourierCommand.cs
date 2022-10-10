using System;
using Atlas.Application.CQRS.Users.Commands.UpdateUser;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCourier
{
    public class UpdateCourierCommand : IRequest
    {
        public Guid Id { get; set; }

        public UpdateUserCommand User { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public int Rate { get; set; }

        public Guid? VehicleId { get; set; }
    }
}
