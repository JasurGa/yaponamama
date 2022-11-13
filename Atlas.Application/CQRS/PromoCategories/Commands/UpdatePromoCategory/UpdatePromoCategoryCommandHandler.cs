using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategories.Commands.UpdatePromoCategory
{
    public class UpdatePromoCategoryCommandHandler : IRequestHandler<UpdatePromoCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdatePromoCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePromoCategoryCommand request, CancellationToken cancellationToken)
        {
            var promoCategory = await _dbContext.PromoCategories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoCategory == null)
            {
                throw new NotFoundException(nameof(PromoCategory), request.Id);
            }

            promoCategory.NameRu   = request.NameRu;
            promoCategory.NameEn   = request.NameEn;
            promoCategory.NameUz   = request.NameUz;
            promoCategory.ImageUrl = request.ImageUrl;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

