﻿using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Commands.CreatePromo
{
    public class CreatePromoCommand : IRequest<Guid>
    {
        public Guid? ClientId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DiscountPrice { get; set; }

        public float DiscountPercent { get; set; }

        public bool ForAllGoods { get; set; }

        public bool FreeDelivery { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
