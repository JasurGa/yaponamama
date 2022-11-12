using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.CreatePromoAdvertisePage
{
    public class CreatePromoAdvertisePageCommandHandler : IRequestHandler<CreatePromoAdvertisePageCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoAdvertisePageCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePromoAdvertisePageCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertise = await _dbContext.PromoAdvertises.FirstOrDefaultAsync(x =>
                x.Id == request.PromoAdvertiseId, cancellationToken);

            if (promoAdvertise == null)
            {
                throw new NotFoundException(nameof(PromoAdvertise), request.PromoAdvertiseId);
            }

            var promoAdvertisePage = new PromoAdvertisePage
            {
                Id               = Guid.NewGuid(),
                PromoAdvertiseId = request.PromoAdvertiseId,
                BadgeColor       = request.BadgeColor,
                BadgeTextRu      = request.BadgeTextRu,
                BadgeTextEn      = request.BadgeTextEn,
                BadgeTextUz      = request.BadgeTextUz,
                TitleColor       = request.TitleColor,
                TitleRu          = request.TitleRu,
                TitleEn          = request.TitleEn,
                TitleUz          = request.TitleUz,
                DescriptionColor = request.DescriptionColor,
                DescriptionRu    = request.DescriptionRu,
                DescriptionEn    = request.DescriptionEn,
                DescriptionUz    = request.DescriptionUz,
                ButtonColor      = request.ButtonColor,
                Background       = request.Background,
                OrderNumber      = request.OrderNumber
            };

            await _dbContext.PromoAdvertisePages.AddAsync(promoAdvertisePage,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return promoAdvertisePage.Id;
        }
    }
}

