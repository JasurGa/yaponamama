using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
