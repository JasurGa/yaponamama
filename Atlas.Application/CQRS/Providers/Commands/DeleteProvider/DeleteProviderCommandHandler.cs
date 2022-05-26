using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Commands.DeleteProvider
{
    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteProviderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.Id);
            }

            _dbContext.Providers.Remove(provider);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
