﻿using System;
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
                x.Id == request.ProviderId);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var good = new Good
            {
                Id            = Guid.NewGuid(),
                Name          = request.Name,
                Description   = request.Description,
                PhotoPath     = request.PhotoPath,
                SellingPrice  = request.SellingPrice,
                PurchasePrice = request.PurchasePrice,
                ProviderId    = request.ProviderId,
                IsDeleted     = false,
            };

            await _dbContext.Goods.AddAsync(good, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return good.Id;
        }
    }
}
