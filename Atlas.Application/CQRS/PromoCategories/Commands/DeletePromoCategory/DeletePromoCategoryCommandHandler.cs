using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategories.Commands.DeletePromoCategory
{
    public class DeletePromoCategoryCommandHandler : IRequestHandler<DeletePromoCategoryCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeletePromoCategoryCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePromoCategoryCommand request, CancellationToken cancellationToken)
        {
            var promoCategory = await _dbContext.PromoCategories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoCategory == null)
            {
                throw new NotFoundException(nameof(PromoCategory), request.Id);
            }

            promoCategory.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

