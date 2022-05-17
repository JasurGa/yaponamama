using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
