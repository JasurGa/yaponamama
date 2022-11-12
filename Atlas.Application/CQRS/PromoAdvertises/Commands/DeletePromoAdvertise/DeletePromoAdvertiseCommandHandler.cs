using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.DeletePromoAdvertise
{
    public class DeletePromoAdvertiseCommandHandler : IRequestHandler<DeletePromoAdvertiseCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeletePromoAdvertiseCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePromoAdvertiseCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertise = await _dbContext.PromoAdvertises.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoAdvertise == null)
            {
                throw new NotFoundException(nameof(PromoAdvertise), request.Id);
            }

            _dbContext.PromoAdvertises.Remove(promoAdvertise);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

