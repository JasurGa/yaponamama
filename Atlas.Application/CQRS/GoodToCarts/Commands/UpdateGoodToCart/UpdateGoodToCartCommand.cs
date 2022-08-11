using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateGoodToCart
{
    public class UpdateGoodToCartCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public int Count { get; set; }
    }
}
