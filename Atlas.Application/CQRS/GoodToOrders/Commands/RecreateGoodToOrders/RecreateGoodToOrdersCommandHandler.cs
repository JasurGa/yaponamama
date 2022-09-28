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
        private readonly IMediator _mediator;
        private readonly IAtlasDbContext _dbContext;

        public RecreateGoodToOrdersCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<List<Guid>> Handle(RecreateGoodToOrdersCommand request, CancellationToken cancellationToken)
        {
            // Check if order exists
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == request.OrderId, cancellationToken);

            if (order == null || order.FinishedAt != null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            // Clear the list of goods of our order and return all the good counts back to stores
            var goodToOrders = await _dbContext.GoodToOrders
                .Where(x => x.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            foreach (var goodToOrder in goodToOrders)
            {
                var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x => 
                    x.GoodId == goodToOrder.GoodId && x.StoreId == order.StoreId, cancellationToken);

                if (storeToGood == null)
                {
                    throw new NotFoundException(nameof(StoreToGood), "No such StoreToGood connection with goodId: " + goodToOrder.GoodId);
                }

                storeToGood.Count += goodToOrder.Count;
            }

            _dbContext.GoodToOrders.RemoveRange(goodToOrders);

            // Create a new list of goods
            foreach (var goodToOrder in request.GoodToOrders)
            {
                goodToOrder.OrderId = order.Id;
                goodToOrder.StoreId = order.StoreId;
                await _mediator.Send(goodToOrder, cancellationToken);
            }

            // Set a new total price
            order.PurchasePrice = await GetPurchasePriceAsync(request, cancellationToken);
            order.SellingPrice  = await GetSellingPriceAsync(request, order.Promo, cancellationToken);

            // Save all the changes
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

                var priceForGoods = good.PurchasePrice * createGoodToOrder.Count;

                calculatedPrice += priceForGoods;
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
    }
}
