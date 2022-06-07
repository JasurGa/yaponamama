using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Commands.DeleteCourier
{
    public class DeleteCourierCommandHandler : IRequestHandler<DeleteCourierCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteCourierCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteCourierCommand request,
            CancellationToken cancellationToken)
        {
            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (courier == null || courier.IsDeleted)
            {
                throw new NotFoundException(nameof(Courier), request.Id);
            }

            courier.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
