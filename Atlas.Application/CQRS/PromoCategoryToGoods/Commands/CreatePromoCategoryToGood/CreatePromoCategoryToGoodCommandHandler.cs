using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoryToGood
{
    public class CreatePromoCategoryToGoodCommandHandler : IRequestHandler<CreatePromoCategoryToGoodCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoCategoryToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePromoCategoryToGoodCommand request, CancellationToken cancellationToken)
        {
            var promoCategoryToGood = await _dbContext.PromoCategoryToGoods.FirstOrDefaultAsync(x =>
                x.GoodId == request.GoodId && x.PromoCategoryId == request.PromoCategoryId,
                cancellationToken);

            if (promoCategoryToGood == null)
            {
                promoCategoryToGood = new PromoCategoryToGood
                {
                    Id              = Guid.NewGuid(),
                    GoodId          = request.GoodId,
                    PromoCategoryId = request.PromoCategoryId
                };

                await _dbContext.PromoCategoryToGoods.AddAsync(promoCategoryToGood,
                    cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return promoCategoryToGood.Id;
        }
    }
}

