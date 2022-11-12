using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.CreatePromoAdvertise
{
    public class CreatePromoAdvertiseCommandHandler : IRequestHandler<CreatePromoAdvertiseCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoAdvertiseCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePromoAdvertiseCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertise = new PromoAdvertise
            {
                Id             = Guid.NewGuid(),
                WideBackground = request.WideBackground,
                HighBackground = request.HighBackground,
                TitleColor     = request.TitleColor,
                TitleRu        = request.TitleRu,
                TitleEn        = request.TitleEn,
                TitleUz        = request.TitleUz,
                OrderNumber    = request.OrderNumber,
                ExpiresAt      = request.ExpiresAt,                
                CreatedAt      = DateTime.UtcNow,
            };

            await _dbContext.PromoAdvertises.AddAsync(promoAdvertise,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return promoAdvertise.Id;
        }
    }
}

