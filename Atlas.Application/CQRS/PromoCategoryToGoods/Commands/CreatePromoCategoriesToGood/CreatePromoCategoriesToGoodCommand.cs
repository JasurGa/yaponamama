using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood
{
    public class CreatePromoCategoriesToGoodCommand : IRequest
    {
        public List<Guid> GoodIds { get; set; }

        public Guid PromoCategoryId { get; set; }
    }
}

