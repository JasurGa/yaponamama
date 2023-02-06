﻿using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Corrections.Commands.CreateCorrection
{
    public class CreateCorrectionCommandHandler : IRequestHandler<CreateCorrectionCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateCorrectionCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCorrectionCommand request,
            CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.GoodId, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.GoodId);
            }

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.StoreId == request.StoreId && x.GoodId == request.GoodId,
                cancellationToken);

            if (storeToGood == null)
            {
                storeToGood = new StoreToGood
                {
                    Id      = Guid.NewGuid(),
                    GoodId  = request.GoodId,
                    StoreId = request.StoreId,
                    Count   = request.Count,
                };

                await _dbContext.StoreToGoods.AddAsync(storeToGood, 
                    cancellationToken);
            }
            else
            {
                storeToGood.Count += request.Count;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var correction = new Correction
            {
                Id            = Guid.NewGuid(),
                UserId        = request.UserId,
                StoreToGoodId = storeToGood.Id,
                CreatedAt     = DateTime.UtcNow,
                Count         = request.Count,
                CauseBy       = request.CauseBy,
            };

            await _dbContext.Corrections.AddAsync(correction,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return correction.Id;
        }
    }
}
