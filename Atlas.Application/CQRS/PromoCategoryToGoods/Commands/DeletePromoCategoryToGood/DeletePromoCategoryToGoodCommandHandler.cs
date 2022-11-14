using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.DeletePromoCategoryToGood
{
    public class DeletePromoCategoryToGoodCommandHandler : IRequestHandler<DeletePromoCategoryToGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeletePromoCategoryToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePromoCategoryToGoodCommand request, CancellationToken cancellationToken)
        {
            var promoCategoryToGood = await _dbContext.PromoCategoryToGoods.FirstOrDefaultAsync(x =>
                x.GoodId == request.GoodId && x.PromoCategoryId == request.PromoCategoryId,
                cancellationToken);

            if (promoCategoryToGood != null)
            {
                _dbContext.PromoCategoryToGoods.Remove(promoCategoryToGood);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}

