using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.PromoCategories.Commands.CreatePromoCategory
{
    public class CreatePromoCategoryCommandHandler : IRequestHandler<CreatePromoCategoryCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoCategoryCommandHandler(IAtlasDbContext dbContext) =>
            dbContext = _dbContext;

        public async Task<Guid> Handle(CreatePromoCategoryCommand request, CancellationToken cancellationToken)
        {
            var promoCategory = new PromoCategory
            {
                Id        = Guid.NewGuid(),
                NameRu    = request.NameRu,
                NameEn    = request.NameEn,
                NameUz    = request.NameUz,
                ImageUrl  = request.ImageUrl,
                IsDeleted = false
            };

            await _dbContext.PromoCategories.AddAsync(promoCategory,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return promoCategory.Id;
        }
    }
}

