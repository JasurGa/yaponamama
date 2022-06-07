using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.CreateGeneralCategory
{
    public class CreateGeneralCategoryCommandHandler
        : IRequestHandler<CreateGeneralCategoryCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateGeneralCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateGeneralCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new GeneralCategory
            {
                Id        = Guid.NewGuid(),
                Name      = request.Name,
                IsDeleted = false
            };

            await _dbContext.GeneralCategories.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
