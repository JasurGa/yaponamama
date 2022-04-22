using System;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCourier
{
    public class UpdateCourierCommand : IRequest
    {
        public Guid Id { get; set; }
        
        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }
    }
}
