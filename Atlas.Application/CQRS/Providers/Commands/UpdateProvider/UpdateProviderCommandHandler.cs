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

namespace Atlas.Application.CQRS.Providers.Commands.UpdateProvider
{
    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateProviderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProviderCommand request,
            CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(p =>
                p.Id == request.Id, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.Id);
            }

            provider.Name         = request.Name;
            provider.Longitude    = request.Longitude;
            provider.Latitude     = request.Latitude;
            provider.Address      = request.Address;
            provider.Description  = request.Description;
            provider.LogotypePath = request.LogotypePath;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
