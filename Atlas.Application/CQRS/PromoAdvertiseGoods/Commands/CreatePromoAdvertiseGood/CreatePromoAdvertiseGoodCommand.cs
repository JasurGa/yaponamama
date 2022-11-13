using System;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.CreatePromoAdvertiseGood
{
    public class CreatePromoAdvertiseGoodCommand : IRequest<Guid>
    {
        public Guid PromoAdvertisePageId { get; set; }

        public Guid GoodId { get; set; }
    }
}

