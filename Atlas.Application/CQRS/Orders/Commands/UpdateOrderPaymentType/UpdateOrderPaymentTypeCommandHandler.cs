using Atlas.Application.Common.Exceptions;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Application.Services;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderPaymentType
{
    public class UpdateOrderPaymentTypeCommandHandler : IRequestHandler<UpdateOrderPaymentTypeCommand>
    {
        private readonly IAtlasDbContext      _dbContext;
        private readonly IBotCallbacksService _botCallbacksService;

        public UpdateOrderPaymentTypeCommandHandler(IAtlasDbContext dbContext, IBotCallbacksService botCallbacksService) =>
            (_dbContext, _botCallbacksService) = (dbContext, botCallbacksService);

        public async Task<Unit> Handle(UpdateOrderPaymentTypeCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            order.PaymentType = request.PaymentType;

            await _dbContext.SaveChangesAsync(cancellationToken);
            if (order.TelegramUserId != null && request.PaymentType == (int)PaymentType.Terminal)
            {
                await _botCallbacksService.SendPaymentAsync(order.TelegramUserId.Value, 
                    order.IsDevVersionBot, order.Id);
            }

            return Unit.Value;
        }
    }
}
