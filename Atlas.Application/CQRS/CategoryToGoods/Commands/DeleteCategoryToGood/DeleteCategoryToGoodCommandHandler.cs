using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood
{
    public class DeleteCategoryToGoodCommandHandler : IRequestHandler<DeleteCategoryToGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteCategoryToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteCategoryToGoodCommand request, CancellationToken cancellationToken)
        {
            var categoryToGood = await _dbContext.CategoryToGoods
                .FirstOrDefaultAsync(ctg => ctg.Id == request.Id, cancellationToken);

            _dbContext.CategoryToGoods.Remove(categoryToGood);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
