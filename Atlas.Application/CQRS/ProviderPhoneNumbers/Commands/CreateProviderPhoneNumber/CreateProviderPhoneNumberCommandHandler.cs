using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumber
{
    public class CreateProviderPhoneNumberCommandHandler : IRequestHandler
        <CreateProviderPhoneNumberCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateProviderPhoneNumberCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProviderPhoneNumberCommand request,
            CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var providerPhoneNumber = new ProviderPhoneNumber
            {
                Id          = Guid.NewGuid(),
                ProviderId  = provider.Id,
                PhoneNumber = request.PhoneNumber,
            };

            await _dbContext.ProviderPhoneNumbers.AddAsync(providerPhoneNumber,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return providerPhoneNumber.Id;
        }
    }
}
