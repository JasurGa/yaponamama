using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoToGoods.Commands.CreatePromoToGood
{
    public class CreatePromoToGoodCommandHandler : IRequestHandler<CreatePromoToGoodCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePromoToGoodCommand request, CancellationToken cancellationToken)
        {
            var promoToGoodWithTheSameParameters = await _dbContext.PromoToGoods.FirstOrDefaultAsync(x =>
                x.PromoId == request.PromoId && x.GoodId == request.GoodId, cancellationToken);

            if (promoToGoodWithTheSameParameters != null)
            {
                return promoToGoodWithTheSameParameters.Id;
            }

            var promo = await _dbContext.Promos.FirstOrDefaultAsync(x =>
                x.Id == request.PromoId, cancellationToken);

            if (promo == null)
            {
                throw new NotFoundException(nameof(Promo), request.PromoId);
            }

            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.GoodId, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.GoodId);
            }

            var promoToGood = new PromoToGood
            {
                Id      = Guid.NewGuid(),
                PromoId = request.PromoId,
                GoodId  = request.GoodId,
            };

            await _dbContext.PromoToGoods.AddAsync(promoToGood, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return promoToGood.Id;
        }
    }
}
