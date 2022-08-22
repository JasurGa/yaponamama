using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Providers.Commands.RestoreProvider
{
    public class RestoreProviderCommandHandler : IRequestHandler<RestoreProviderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreProviderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreProviderCommand request,
            CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.Id);
            }

            provider.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
