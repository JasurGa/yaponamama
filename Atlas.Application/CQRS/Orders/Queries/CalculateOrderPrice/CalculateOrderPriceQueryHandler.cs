﻿using Atlas.Application.CQRS.Orders.Commands.CreateOrder;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice
{
    public class CalculateOrderPriceQueryHandler : IRequestHandler<CalculateOrderPriceQuery, PriceDetailsVm>
    {
        private readonly static int DELIVERY_PRICE = 15_000;

        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public CalculateOrderPriceQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        private async Task<Promo> GetPromoAsync(CalculateOrderPriceQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Promo == null)
            {
                return null;
            }

            var foundPromo = await _dbContext.Promos.FirstOrDefaultAsync(x =>
                x.Name == request.Promo, cancellationToken);

            if (foundPromo == null || foundPromo.ExpiresAt <= DateTime.UtcNow)
            {
                return null;
            }

            if (foundPromo.ClientId != null && foundPromo.ClientId != request.ClientId)
            {
                return null;
            }

            var promoUsage = await _dbContext.PromoUsages.FirstOrDefaultAsync(x =>
                x.PromoId == foundPromo.Id && x.ClientId == request.ClientId);

            if (promoUsage != null)
            {
                return null;
            }

            return foundPromo;
        }

        private async Task<float> GetSellingPriceAsync(CalculateOrderPriceQuery request,
            CancellationToken cancellationToken, Promo promo)   
        {
            var calculatedPrice = 0.0f;

            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == createGoodToOrder.GoodId, cancellationToken);

                var priceForGoods = good.SellingPrice * createGoodToOrder.Count *
                    (1 - good.Discount);

                calculatedPrice += priceForGoods;
            }

            if (promo != null)
            {
                calculatedPrice *= (1 - promo.DiscountPercent);
                calculatedPrice -= promo.DiscountPrice;
            }

            return calculatedPrice;
        }

        private async Task<float> GetShippingPriceAsync(CalculateOrderPriceQuery request,
            CancellationToken cancellationToken)
        {
            return request.IsPickup ? 0.0f : DELIVERY_PRICE;
        }

        public async Task<PriceDetailsVm> Handle(CalculateOrderPriceQuery request,
            CancellationToken cancellationToken)
        {
            var foundPromo      = await GetPromoAsync(request, cancellationToken);
            var sellingPrice    = await GetSellingPriceAsync(request, cancellationToken, foundPromo);
            var shippingPrice   = await GetShippingPriceAsync(request, cancellationToken);

            return new PriceDetailsVm
            {
                SellingPrice  = sellingPrice,
                ShippingPrice = shippingPrice
            };
        }
    }
}