using System;
using MediatR;

namespace Atlas.Application.CQRS.Couriers.Commands.DeleteCourier
{
    public class DeleteCourierCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
