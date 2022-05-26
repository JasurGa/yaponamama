using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteStoreCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.Id);
            }

            store.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
