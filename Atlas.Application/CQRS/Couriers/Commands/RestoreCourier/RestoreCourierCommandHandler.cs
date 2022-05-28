using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Couriers.Commands.RestoreCourier
{
    public class RestoreCourierCommandHandler : IRequestHandler<RestoreCourierCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreCourierCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (courier == null)
            {
                throw new NotFoundException(nameof(Courier), request.Id);
            }

            courier.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
