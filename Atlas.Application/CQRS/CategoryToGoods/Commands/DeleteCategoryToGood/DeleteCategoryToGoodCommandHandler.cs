using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
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

            if (categoryToGood == null)
            {
                throw new NotFoundException(nameof(CategoryToGood), request.Id);
            }

            _dbContext.CategoryToGoods.Remove(categoryToGood);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
