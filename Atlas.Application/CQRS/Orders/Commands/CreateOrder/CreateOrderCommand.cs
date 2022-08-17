using System;
using System.Collections.Generic;
using Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public int DestinationType { get; set; }

        public int Floor { get; set; }

        public int Entrance { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public Guid PaymentTypeId { get; set; }

        public string Promo { get; set; }

        public IEnumerable<CreateGoodToOrderCommand> GoodToOrders { get; set; }
    }
}
