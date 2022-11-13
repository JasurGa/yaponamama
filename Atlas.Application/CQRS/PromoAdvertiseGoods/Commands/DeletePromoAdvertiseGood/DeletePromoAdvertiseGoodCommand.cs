using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.DeletePromoAdvertiseGood
{
    public class DeletePromoAdvertiseGoodCommand : IRequest
    {
        public Guid PromoAdvertisePageId { get; set; }

        public Guid GoodId { get; set; }
    }
}

