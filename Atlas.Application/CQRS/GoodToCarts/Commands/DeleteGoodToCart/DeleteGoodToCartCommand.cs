using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.DeleteGoodToCart
{
    public class DeleteGoodToCartCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
