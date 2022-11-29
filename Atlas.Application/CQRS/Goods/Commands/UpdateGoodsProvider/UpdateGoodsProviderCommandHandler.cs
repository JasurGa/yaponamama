using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Commands.UpdateGoodsProvider
{
    public class UpdateGoodsProviderCommandHandler : IRequestHandler<UpdateGoodsProviderCommand>
    {
        private readonly IAtlasDbContext _dbContext;
        public UpdateGoodsProviderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateGoodsProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x => 
                x.Id == request.ProviderId, 
                    cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            foreach (var goodId in request.GoodIds)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x => 
                    x.Id == goodId, cancellationToken);
                
                if (good == null)
                {
                    throw new NotFoundException(nameof(Good), goodId);
                }

                good.ProviderId = request.ProviderId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
