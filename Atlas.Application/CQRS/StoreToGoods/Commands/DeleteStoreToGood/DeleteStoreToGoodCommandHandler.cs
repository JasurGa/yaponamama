using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood
{
    public class DeleteStoreToGoodCommandHandler : IRequestHandler<DeleteStoreToGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteStoreToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteStoreToGoodCommand request,
            CancellationToken cancellationToken)
        {
            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (storeToGood == null)
            {
                throw new NotFoundException(nameof(StoreToGood), request.Id);
            }

            _dbContext.StoreToGoods.Remove(storeToGood);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
