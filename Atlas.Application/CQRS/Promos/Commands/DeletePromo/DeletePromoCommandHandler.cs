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
            var promo = await _dbContext.Promos.FirstOrDefaultAsync(p =>
                p.Id == request.Id, cancellationToken);

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
