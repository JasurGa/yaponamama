using System;
using System.Collections.Generic;
using Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string? Comment { get; set; }
        
        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public Guid PaymentTypeId { get; set; }

        public string Promo { get; set; }

        public IList<CreateGoodToOrderCommand> GoodToOrders { get; set; }
    }
}
