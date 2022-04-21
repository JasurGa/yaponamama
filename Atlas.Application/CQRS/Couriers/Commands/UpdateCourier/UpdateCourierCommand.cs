using System;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCourier
{
    public class UpdateCourierCommand : IRequest
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public Guid VehicleId { get; set; }
    }
}
