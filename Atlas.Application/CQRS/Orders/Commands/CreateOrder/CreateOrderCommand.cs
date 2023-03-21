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

        public string Apartment { get; set; }

        public string Floor { get; set; }

        public string Entrance { get; set; }

        public string Address { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public int PaymentType { get; set; }

        public string Promo { get; set; }

        public DateTime? DeliverAt { get; set; }

        public long? TelegramUserId { get; set; }

        public bool IsDevVersionBot { get; set; }

        public int GoodReplacementType { get; set; }

        public bool IsPrivateHouse { get; set; }

        public IEnumerable<CreateGoodToOrderCommand> GoodToOrders { get; set; }
    }
}
