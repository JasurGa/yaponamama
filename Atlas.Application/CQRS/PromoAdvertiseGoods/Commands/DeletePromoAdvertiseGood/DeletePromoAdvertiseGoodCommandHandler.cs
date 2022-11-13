using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.DeletePromoAdvertiseGood
{
    public class DeletePromoAdvertiseGoodCommandHandler : IRequestHandler<DeletePromoAdvertiseGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeletePromoAdvertiseGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePromoAdvertiseGoodCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertiseGood = await _dbContext.PromoAdvertiseGoods.FirstOrDefaultAsync(x =>
                x.GoodId == request.GoodId && x.PromoAdvertisePageId == request.PromoAdvertisePageId,
                cancellationToken);

            if (promoAdvertiseGood == null)
            {
                throw new NotFoundException(nameof(PromoAdvertiseGood), request.GoodId);
            }

            _dbContext.PromoAdvertiseGoods.Remove(promoAdvertiseGood);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

