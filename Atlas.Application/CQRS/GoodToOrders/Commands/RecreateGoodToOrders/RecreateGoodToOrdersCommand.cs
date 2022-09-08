using Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder;
using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.RecreateGoodToOrders
{
    public class RecreateGoodToOrdersCommand : IRequest<List<Guid>>
    {
        public Guid OrderId { get; set; }

        public IList<CreateGoodToOrderCommand> GoodToOrders { get; set; }
    }
}
