using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Application.Services;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IAtlasDbContext      _dbContext;
        private readonly IBotCallbacksService _botCallbacksService;

        public UpdateOrderStatusCommandHandler(IAtlasDbContext dbContext, IBotCallbacksService botCallbacksService) =>
            (_dbContext, _botCallbacksService) = (dbContext, botCallbacksService);

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(x => x.GoodToOrders).FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            if (order.Status == (int)OrderStatus.Success && request.Status != (int)OrderStatus.Success)
            {
                var goodIds = order.GoodToOrders.Select(x => x.GoodId);

                var storeToGoods = await _dbContext.StoreToGoods.Where(x => x.StoreId == order.StoreId &&
                    goodIds.Contains(x.GoodId)).ToListAsync(cancellationToken);

                foreach (var storeToGood in storeToGoods)
                {
                    var goodToOrder = order.GoodToOrders.FirstOrDefault(x => x.GoodId == storeToGood.GoodId);
                    if (goodToOrder != null)
                    {
                        storeToGood.Count += goodToOrder.Count;
                    }
                }
            }
            else if (order.Status != (int)OrderStatus.Success && request.Status == (int)OrderStatus.Success)
            {
                var goodIds = order.GoodToOrders.Select(x => x.GoodId);

                var storeToGoods = await _dbContext.StoreToGoods.Where(x => x.StoreId == order.StoreId &&
                    goodIds.Contains(x.GoodId)).ToListAsync(cancellationToken);

                foreach (var storeToGood in storeToGoods)
                {
                    var goodToOrder = order.GoodToOrders.FirstOrDefault(x => x.GoodId == storeToGood.GoodId);
                    if (goodToOrder != null)
                    {
                        storeToGood.Count -= goodToOrder.Count;
                        if (storeToGood.Count < 0)
                        {
                            storeToGood.Count = 0;
                        }
                    }
                }
            }

            if (order.Status != request.Status)
            {
                order.StatusLastEditedAt = DateTime.UtcNow;

                if (request.Status == (int)OrderStatus.Success || request.Status == (int)OrderStatus.CanceledByAdmin || request.Status == (int)OrderStatus.CanceledByUser)
                {
                    order.FinishedAt = DateTime.UtcNow;
                }

                order.Status = request.Status;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            if (order.TelegramUserId != null)
            {
                await _botCallbacksService.UpdateStatusAsync(order.TelegramUserId.Value, order.IsDevVersionBot,
                    order.Id, order.Status);
            }

            return Unit.Value;
        }
    }
}

