using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.DeleteGoodToOrder
{
    public class DeleteGoodToOrderCommandHandler : IRequestHandler<DeleteGoodToOrderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteGoodToOrderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteGoodToOrderCommand request, CancellationToken cancellationToken)
        {
            var goodToOrder = await _dbContext.GoodToOrders.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (goodToOrder == null)
            {
                throw new NotFoundException(nameof(GoodToOrder), request.Id);
            }

            _dbContext.GoodToOrders.Remove(goodToOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
