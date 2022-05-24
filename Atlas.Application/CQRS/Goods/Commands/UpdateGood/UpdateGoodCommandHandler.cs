﻿using System;
using System.Collections.Generic;
using System.Threading;
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
            var good = await _dbContext.Goods
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.Id);
            }

            good.Name           = request.Name;
            good.Description    = request.Description;
            good.PhotoPath      = request.PhotoPath;
            good.SellingPrice   = request.SellingPrice;
            good.PurchasePrice  = request.PurchasePrice;
            good.ProviderId     = request.ProviderId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}