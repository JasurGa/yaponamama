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

namespace Atlas.Application.CQRS.PromoToGoods.Commands.UpdatePromoToGood
{
    public class UpdatePromoToGoodCommandHandler : IRequestHandler<UpdatePromoToGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdatePromoToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePromoToGoodCommand request, CancellationToken cancellationToken)
        {
            var promoToGood = await _dbContext.PromoToGoods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoToGood == null)
            {
                throw new NotFoundException(nameof(PromoToGood), request.Id);
            }

            promoToGood.GoodId  = request.GoodId;
            promoToGood.PromoId = request.PromoId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
