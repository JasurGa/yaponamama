using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Commands.CreateProvider
{
    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateProviderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = new Provider
            {
                Id           = Guid.NewGuid(),
                Name         = request.Name,
                Longitude    = request.Longitude,
                Latitude     = request.Latitude,
                Address      = request.Address,
                Description  = request.Description,
                LogotypePath = request.LogotypePath
            };

            await _dbContext.Providers.AddAsync(provider, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return provider.Id;
        }
    }
}
