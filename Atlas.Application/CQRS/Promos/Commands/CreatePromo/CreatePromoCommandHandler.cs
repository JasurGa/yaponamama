using Atlas.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Exceptions;

namespace Atlas.Application.CQRS.Promos.Commands.CreatePromo
{
    public class CreatePromoCommandHandler : IRequestHandler<CreatePromoCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePromoCommandHandler(IAtlasDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePromoCommand request,
            CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.GoodId, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.GoodId);
            }

            var promo = new Promo
            {
                Id              = Guid.NewGuid(),
                GoodId          = request.GoodId,
                Name            = request.Name,
                DiscountPrice   = request.DiscountPrice,
                DiscountPercent = request.DiscountPercent,
            };

            await _dbContext.Promos.AddAsync(promo, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return promo.Id;
        }
    }
}
