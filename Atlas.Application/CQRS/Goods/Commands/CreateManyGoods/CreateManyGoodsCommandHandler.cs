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

namespace Atlas.Application.CQRS.Goods.Commands.CreateManyGoods
{
    public class CreateManyGoodsCommandHandler : IRequestHandler<CreateManyGoodsCommand, List<Guid>>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateManyGoodsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<List<Guid>> Handle(CreateManyGoodsCommand request, CancellationToken cancellationToken)
        {
            foreach (var goodRequest in request.Goods)
            {
                var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                     x.Id == goodRequest.ProviderId, cancellationToken);

                if (provider == null)
                {
                    throw new NotFoundException(nameof(Provider), goodRequest.ProviderId);
                }
            }

            var goodIds = new List<Guid>();
            foreach (var goodRequest in request.Goods)
            {
                var good = new Good
                {
                    Id             = Guid.NewGuid(),
                    Name           = goodRequest.Name,
                    NameRu         = goodRequest.NameRu,
                    NameEn         = goodRequest.NameEn,
                    NameUz         = goodRequest.NameUz,
                    Description    = goodRequest.Description,
                    DescriptionRu  = goodRequest.DescriptionRu,
                    DescriptionEn  = goodRequest.DescriptionEn,
                    DescriptionUz  = goodRequest.DescriptionUz,
                    NoteRu         = goodRequest.NoteRu,
                    NoteEn         = goodRequest.NoteEn,
                    NoteUz         = goodRequest.NoteUz,
                    PhotoPath      = goodRequest.PhotoPath,
                    SellingPrice   = goodRequest.SellingPrice,
                    PurchasePrice  = goodRequest.PurchasePrice,
                    ProviderId     = goodRequest.ProviderId,
                    Volume         = goodRequest.Volume,
                    Mass           = goodRequest.Mass,
                    Discount       = goodRequest.Discount,
                    IsDeleted      = false,
                    CreatedAt      = DateTime.UtcNow,
                    CodeIkpu       = goodRequest.CodeIkpu,
                    SaleTaxPercent = goodRequest.SaleTaxPercent,
                    PackageCode    = goodRequest.PackageCode,
                    IsVerified     = goodRequest.IsVerified
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

                goodIds.Add(good.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return goodIds;
        }
    }
}
