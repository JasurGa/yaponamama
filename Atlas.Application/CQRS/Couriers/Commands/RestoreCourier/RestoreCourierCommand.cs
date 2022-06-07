using MediatR;
using System;

namespace Atlas.Application.CQRS.Couriers.Commands.RestoreCourier
{
    public class RestoreCourierCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
