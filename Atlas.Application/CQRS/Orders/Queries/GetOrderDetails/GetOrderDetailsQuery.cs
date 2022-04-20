using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQuery : IRequest<OrderDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
