using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Promos.Commands.DeletePromo
{
    public class DeletePromoCommandHandler : IRequestHandler<DeletePromoCommand>
    {
        public readonly IAtlasDbContext _dbContext;

        public DeletePromoCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePromoCommand request, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders.Where(x => x.PromoId == request.Id)
                .ToListAsync(cancellationToken);

            foreach (var order in orders)
            {
                order.PromoId = null;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var promo = await _dbContext.Promos.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promo == null)
            {
                throw new NotFoundException(nameof(Promo), request.Id);
            }

            _dbContext.Promos.Remove(promo);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
