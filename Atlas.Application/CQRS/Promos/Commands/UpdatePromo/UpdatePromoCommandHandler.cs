using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Promos.Commands.UpdatePromo
{
    public class UpdatePromoCommandHandler : IRequestHandler<UpdatePromoCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdatePromoCommandHandler(IAtlasDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePromoCommand request,
            CancellationToken cancellationToken)
        {
            var promo = await _dbContext.Promos.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promo == null)
            {
                throw new NotFoundException(nameof(Promo), request.Id);
            }

            promo.ClientId        = request.ClientId;
            promo.Name            = request.Name;
            promo.DiscountPercent = request.DiscountPercent;
            promo.DiscountPrice   = request.DiscountPrice;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
