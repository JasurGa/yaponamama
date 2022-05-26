using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood
{
    public class CreateCategoryToGoodCommandHandler : IRequestHandler<CreateCategoryToGoodCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateCategoryToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCategoryToGoodCommand request,
            CancellationToken cancellationToken)
        {
            var categoryToGood = new CategoryToGood
            {
                Id          = Guid.NewGuid(),
                GoodId      = request.GoodId,
                CategoryId  = request.CategoryId,
            };

            await _dbContext.CategoryToGoods.AddAsync(categoryToGood,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return categoryToGood.Id;
        }
    }
}
