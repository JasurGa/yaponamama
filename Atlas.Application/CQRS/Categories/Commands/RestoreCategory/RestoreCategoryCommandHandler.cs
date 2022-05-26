using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Categories.Commands.RestoreCategory
{
    public class RestoreCategoryCommandHandler : IRequestHandler<RestoreCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null || !category.IsDeleted)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
