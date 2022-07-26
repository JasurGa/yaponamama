using System;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetails;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForAdmin
{
    public class GetOrderDetailsForAdminQuery : IRequest<OrderDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
