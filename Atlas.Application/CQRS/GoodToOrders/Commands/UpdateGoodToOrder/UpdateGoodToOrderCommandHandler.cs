using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.UpdateGoodToOrder
{
    public class UpdateGoodToOrderCommandHandler : IRequestHandler<UpdateGoodToOrderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateGoodToOrderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateGoodToOrderCommand request, CancellationToken cancellationToken)
        {
            var goodToOrder = await _dbContext.GoodToOrders.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (goodToOrder == null)
            {
                throw new NotFoundException(nameof(GoodToOrder), request.Id);
            }

            goodToOrder.Count = request.Count;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
