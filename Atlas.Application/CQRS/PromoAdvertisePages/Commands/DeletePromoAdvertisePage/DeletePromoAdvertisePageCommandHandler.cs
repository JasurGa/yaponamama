using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.DeletePromoAdvertisePage
{
    public class DeletePromoAdvertisePageCommandHandler : IRequestHandler<DeletePromoAdvertisePageCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeletePromoAdvertisePageCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePromoAdvertisePageCommand request, CancellationToken cancellationToken)
        {
            var promoAdvertisePage = await _dbContext.PromoAdvertisePages.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoAdvertisePage == null)
            {
                throw new NotFoundException(nameof(PromoAdvertisePage), request.Id);
            }

            _dbContext.PromoAdvertisePages.Remove(promoAdvertisePage);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

