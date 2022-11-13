using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.CreatePromoAdvertiseGood
{
    public class CreatePromoAdvertiseGoodCommandHandler : IRequestHandler<CreatePromoAdvertiseGoodCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoAdvertiseGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePromoAdvertiseGoodCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertiseGood = await _dbContext.PromoAdvertiseGoods.FirstOrDefaultAsync(x =>
                x.GoodId == request.GoodId && x.PromoAdvertisePageId == request.PromoAdvertisePageId,
                cancellationToken);

            if (promoAdvertiseGood == null)
            {
                promoAdvertiseGood = new PromoAdvertiseGood
                {
                    Id                   = Guid.NewGuid(),
                    GoodId               = request.GoodId,
                    PromoAdvertisePageId = request.PromoAdvertisePageId
                };

                await _dbContext.PromoAdvertiseGoods.AddAsync(promoAdvertiseGood,
                    cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return promoAdvertiseGood.Id;
        }
    }
}

