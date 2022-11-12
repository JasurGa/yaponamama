using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.UpdatePromoAdvertisePage
{
    public class UpdatePromoAdvertisePageCommandHandler : IRequestHandler<UpdatePromoAdvertisePageCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdatePromoAdvertisePageCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePromoAdvertisePageCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertise = await _dbContext.PromoAdvertises.FirstOrDefaultAsync(x =>
                x.Id == request.PromoAdvertiseId, cancellationToken);

            if (promoAdvertise == null)
            {
                throw new NotFoundException(nameof(PromoAdvertise), request.PromoAdvertiseId);
            }

            var promoAdvertisePage = await _dbContext.PromoAdvertisePages.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoAdvertisePage == null)
            {
                throw new NotFoundException(nameof(PromoAdvertisePage), request.Id);
            }

            promoAdvertisePage.PromoAdvertiseId = request.PromoAdvertiseId;
            promoAdvertisePage.BadgeColor       = request.BadgeColor;
            promoAdvertisePage.BadgeTextRu      = request.BadgeTextRu;
            promoAdvertisePage.BadgeTextEn      = request.BadgeTextEn;
            promoAdvertisePage.BadgeTextUz      = request.BadgeTextUz;
            promoAdvertisePage.TitleColor       = request.TitleColor;
            promoAdvertisePage.TitleRu          = request.TitleRu;
            promoAdvertisePage.TitleEn          = request.TitleEn;
            promoAdvertisePage.TitleUz          = request.TitleUz;
            promoAdvertisePage.DescriptionColor = request.DescriptionColor;
            promoAdvertisePage.DescriptionRu    = request.DescriptionRu;
            promoAdvertisePage.DescriptionEn    = request.DescriptionEn;
            promoAdvertisePage.DescriptionUz    = request.DescriptionUz;
            promoAdvertisePage.ButtonColor      = request.ButtonColor;
            promoAdvertisePage.Background       = request.Background;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

