using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Stores.Commands.RestoreStore
{
    public class RestoreStoreCommandHandler : IRequestHandler<RestoreStoreCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreStoreCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _dbContext.Stores
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.Id);
            }

            store.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
