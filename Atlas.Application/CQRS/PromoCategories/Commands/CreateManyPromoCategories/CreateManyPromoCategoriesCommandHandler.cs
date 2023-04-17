using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoCategories.Commands.CreateManyPromoCategories
{
    public class CreateManyPromoCategoriesCommandHandler : IRequestHandler<CreateManyPromoCategoriesCommand, List<Guid>>
    {
        private readonly IAtlasDbContext _dbContext;
        public CreateManyPromoCategoriesCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<List<Guid>> Handle(CreateManyPromoCategoriesCommand request, CancellationToken cancellationToken)
        {
            var promoCategoryIds = new List<Guid>();

            foreach (var requestPromoCategory in request.PromoCategories)
            {
                var promoCategory = new PromoCategory
                {
                    Id       = Guid.NewGuid(),
                    NameEn   = requestPromoCategory.NameEn,
                    NameRu   = requestPromoCategory.NameRu,
                    NameUz   = requestPromoCategory.NameUz,
                    ImageUrl = requestPromoCategory.ImageUrl,
                };

                await _dbContext.PromoCategories.AddAsync(promoCategory);

                promoCategoryIds.Add(promoCategory.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return promoCategoryIds;
        }
    }
}
