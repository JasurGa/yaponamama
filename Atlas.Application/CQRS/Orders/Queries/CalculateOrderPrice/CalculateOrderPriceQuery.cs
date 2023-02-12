using Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice
{
    public class CalculateOrderPriceQuery : IRequest<PriceDetailsVm>
    {
        public Guid ClientId { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public string Promo { get; set; }

        public IEnumerable<CreateGoodToOrderCommand> GoodToOrders { get; set; }
    }
}
