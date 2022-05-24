﻿using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Commands.CreatePromo
{
    public class CreatePromoCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }
    }
}