using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.CreateGoodToCart
{
    public class CreateGoodToCartCommand : IRequest<Guid>
    {
        public Guid GoodId { get; set; }

        public Guid ClientId { get; set; }

        public int Count { get; set; }
    }
}