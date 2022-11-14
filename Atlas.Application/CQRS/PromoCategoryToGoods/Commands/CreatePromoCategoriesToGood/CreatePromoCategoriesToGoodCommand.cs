using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood
{
    public class CreatePromoCategoriesToGoodCommand : IRequest
    {
        public Guid GoodId { get; set; }

        public List<Guid> PromoCategoryIds { get; set; }
    }
}

