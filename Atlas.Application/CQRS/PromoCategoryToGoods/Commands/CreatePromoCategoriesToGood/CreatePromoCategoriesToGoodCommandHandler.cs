﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood
{
    public class CreatePromoCategoriesToGoodCommandHandler : IRequestHandler<CreatePromoCategoriesToGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoCategoriesToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreatePromoCategoriesToGoodCommand request, CancellationToken cancellationToken)
        {
            foreach (var categoryId in request.PromoCategoryIds)
            {
                var promoCategoryToGood = await _dbContext.PromoCategoryToGoods.FirstOrDefaultAsync(x =>
                    x.GoodId == request.GoodId && x.PromoCategoryId == categoryId);

                if (promoCategoryToGood == null)
                {
                    await _dbContext.PromoCategoryToGoods.AddAsync(new PromoCategoryToGood
                    {
                        Id              = Guid.NewGuid(),
                        GoodId          = request.GoodId,
                        PromoCategoryId = categoryId
                    });
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

