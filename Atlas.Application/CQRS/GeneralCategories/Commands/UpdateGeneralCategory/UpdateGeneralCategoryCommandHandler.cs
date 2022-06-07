using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.UpdateGeneralCategory
{
    public class UpdateGeneralCategoryCommandHandler :
        IRequestHandler<UpdateGeneralCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateGeneralCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateGeneralCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var generalCategory = await _dbContext.GeneralCategories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (generalCategory == null)
            {
                throw new NotFoundException(nameof(GeneralCategory), request.Id);
            }

            generalCategory.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
