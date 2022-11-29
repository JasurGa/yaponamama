using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Commands.UpdateGoodsProvider
{
    public class UpdateGoodsProviderCommand : IRequest
    {
        public Guid ProviderId { get; set; }

        public List<Guid> GoodIds { get; set; }
    }
}
