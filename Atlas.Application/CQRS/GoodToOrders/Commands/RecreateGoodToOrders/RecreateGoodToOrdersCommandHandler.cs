using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.RecreateGoodToOrders
{
    public class RecreateGoodToOrdersCommandHandler : IRequestHandler<RecreateGoodToOrdersCommand, List<Guid>>
    {
        private readonly IAtlasDbContext _dbContext;

        public RecreateGoodToOrdersCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<List<Guid>> Handle(RecreateGoodToOrdersCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            var goodToOrders = await _dbContext.GoodToOrders
                .Where(x => x.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            foreach (var goodToOrder in goodToOrders)
            {
                var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x => 
                    x.GoodId == goodToOrder.GoodId && x.StoreId == order.StoreId,
                    cancellationToken);

                if (storeToGood != null)
                {
                    storeToGood.Count += goodToOrder.Count;
                }
            }

            _dbContext.GoodToOrders.RemoveRange(goodToOrders);
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var goodToOrder in request.GoodToOrders)
            {
                goodToOrder.OrderId = order.Id;
                goodToOrder.StoreId = order.StoreId;

                var e = new GoodToOrder
                {
                    Id      = Guid.NewGuid(),
                    GoodId  = goodToOrder.GoodId,
                    OrderId = goodToOrder.OrderId,
                    Count   = goodToOrder.Count,
                };

                await _dbContext.GoodToOrders.AddAsync(e, cancellationToken);

                var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                    x.GoodId == goodToOrder.GoodId && x.StoreId == order.StoreId,
                    cancellationToken);

                if (storeToGood != null)
                {
                    storeToGood.Count -= goodToOrder.Count;
                }
            }

            order.PurchasePrice = await GetPurchasePriceAsync(request, cancellationToken);
            order.SellingPrice  = await GetSellingPriceAsync(request, order.Promo, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return await _dbContext.GoodToOrders
                .Where(x => x.OrderId == request.OrderId)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        private async Task<float> GetPurchasePriceAsync(RecreateGoodToOrdersCommand request, CancellationToken cancellationToken)
        {
            var calculatedPrice = 0.0f;
            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == createGoodToOrder.GoodId, cancellationToken);

                if (good != null)
                {
                    var priceForGoods = good.PurchasePrice * createGoodToOrder.Count;
                    calculatedPrice += priceForGoods;
                }
            }

            return calculatedPrice;
        }

        private async Task<float> GetSellingPriceAsync(RecreateGoodToOrdersCommand request, Promo promo, CancellationToken cancellationToken)
        {
            var calculatedPrice = 0.0f;
            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == createGoodToOrder.GoodId, cancellationToken);

                if (good != null)
                {
                    var priceForGoods = good.SellingPrice * createGoodToOrder.Count *
                        (1 - good.Discount);

                    calculatedPrice += priceForGoods;
                }
            }

            if (promo != null)
            {
                calculatedPrice *= (1 - promo.DiscountPercent);
                calculatedPrice -= promo.DiscountPrice;
            }

            return calculatedPrice;
        }
    }
}
