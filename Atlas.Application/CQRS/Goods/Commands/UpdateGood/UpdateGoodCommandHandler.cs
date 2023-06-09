﻿using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Commands.UpdateGood
{
    public class UpdateGoodCommandHandler : IRequestHandler<UpdateGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateGoodCommand request, CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.Id);
            }

            good.Name           = request.Name;
            good.NameRu         = request.NameRu;
            good.NameEn         = request.NameEn;
            good.NameUz         = request.NameUz;
            good.Description    = request.Description;
            good.DescriptionRu  = request.DescriptionRu;
            good.DescriptionEn  = request.DescriptionEn;
            good.DescriptionUz  = request.DescriptionUz;
            good.NoteRu         = request.NoteRu;
            good.NoteEn         = request.NoteEn;
            good.NoteUz         = request.NoteUz;
            good.PhotoPath      = request.PhotoPath;
            good.SellingPrice   = request.SellingPrice;
            good.PurchasePrice  = request.PurchasePrice;
            good.ProviderId     = request.ProviderId;
            good.Mass           = request.Mass;
            good.Volume         = request.Volume;
            good.Discount       = request.Discount;
            good.SaleTaxPercent = request.SaleTaxPercent;
            good.CodeIkpu       = request.CodeIkpu;
            good.PackageCode    = request.PackageCode;
            good.IsVerified     = request.IsVerified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
