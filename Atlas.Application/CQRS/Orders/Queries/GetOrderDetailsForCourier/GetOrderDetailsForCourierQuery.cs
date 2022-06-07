using System;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetails;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForCourier
{
    public class GetOrderDetailsForCourierQuery : IRequest<OrderDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid CourierId { get; set; }
    }
}
