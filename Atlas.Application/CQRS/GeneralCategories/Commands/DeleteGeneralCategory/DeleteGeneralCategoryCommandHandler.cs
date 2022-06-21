using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.DeleteGeneralCategory
{
    public class DeleteGeneralCategoryCommandHandler :
        IRequestHandler<DeleteGeneralCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteGeneralCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteGeneralCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _dbContext.GeneralCategories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (category == null || category.IsDeleted)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
