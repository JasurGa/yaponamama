using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Commands.DiscountGoods
{
    public class DiscountGoodsCommandHandler : IRequestHandler<DiscountGoodsCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DiscountGoodsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DiscountGoodsCommand request, CancellationToken cancellationToken)
        {
            foreach (var goodId in request.GoodIds)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == goodId, cancellationToken);

                if (good == null)
                {
                    throw new NotFoundException(nameof(Good), goodId);
                }

                good.Discount = request.Discount;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
