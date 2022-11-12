using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.UpdatePromoAdvertise
{
    public class UpdatePromoAdvertiseCommandHandler : IRequestHandler<UpdatePromoAdvertiseCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdatePromoAdvertiseCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePromoAdvertiseCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertise = await _dbContext.PromoAdvertises.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoAdvertise == null)
            {
                throw new NotFoundException(nameof(PromoAdvertise), request.Id);
            }

            promoAdvertise.WideBackground = request.WideBackground;
            promoAdvertise.HighBackground = request.HighBackground;
            promoAdvertise.TitleColor     = request.TitleColor;
            promoAdvertise.TitleRu        = request.TitleRu;
            promoAdvertise.TitleEn        = request.TitleEn;
            promoAdvertise.TitleUz        = request.TitleUz;
            promoAdvertise.OrderNumber    = request.OrderNumber;
            promoAdvertise.ExpiresAt      = request.ExpiresAt;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

