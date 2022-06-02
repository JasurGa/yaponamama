using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.RestoreGeneralCategory
{
    public class RestoreGeneralCategoryCommandHandler :
        IRequestHandler<RestoreGeneralCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreGeneralCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreGeneralCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _dbContext.GeneralCategories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

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
