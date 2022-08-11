using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.DeleteGoodToCart
{
    public class DeleteGoodToCartCommandHandler : IRequestHandler<DeleteGoodToCartCommand>
    {
        private readonly IAtlasDbContext _dbContext;
        public DeleteGoodToCartCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteGoodToCartCommand request, CancellationToken cancellationToken)
        {
            var goodToCart = await _dbContext.GoodToCarts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (goodToCart == null || goodToCart.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(GoodToCart), request.Id);
            }

            _dbContext.GoodToCarts.Remove(goodToCart);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
