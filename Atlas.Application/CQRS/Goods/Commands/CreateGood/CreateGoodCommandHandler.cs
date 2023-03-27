using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Commands.CreateGood
{
    public class CreateGoodCommandHandler : IRequestHandler<CreateGoodCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateGoodCommand request, CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var good = new Good
            {
                Id             = Guid.NewGuid(),
                Name           = request.Name,
                NameRu         = request.NameRu,
                NameEn         = request.NameEn,
                NameUz         = request.NameUz,
                Description    = request.Description,
                DescriptionRu  = request.DescriptionRu,
                DescriptionEn  = request.DescriptionEn,
                DescriptionUz  = request.DescriptionUz,
                NoteRu         = request.NoteRu,
                NoteEn         = request.NoteEn,
                NoteUz         = request.NoteUz,
                PhotoPath      = request.PhotoPath,
                SellingPrice   = request.SellingPrice,
                PurchasePrice  = request.PurchasePrice,
                ProviderId     = request.ProviderId,
                Volume         = request.Volume,
                Mass           = request.Mass,
                Discount       = request.Discount,
                IsDeleted      = false,
                CreatedAt      = DateTime.UtcNow,
                CodeIkpu       = request.CodeIkpu,
                SaleTaxPercent = request.SaleTaxPercent,
                PackageCode    = request.PackageCode,
                IsVerified     = request.IsVerified
            };

            await _dbContext.Goods.AddAsync(good, cancellationToken);

            var stores = await _dbContext.Stores.ToListAsync();
            foreach (var store in stores)
            {
                await _dbContext.StoreToGoods.AddAsync(new StoreToGood
                {
                    Id      = Guid.NewGuid(),
                    GoodId  = good.Id,
                    StoreId = store.Id,
                    Count   = 0,
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return good.Id;
        }
    }
}
