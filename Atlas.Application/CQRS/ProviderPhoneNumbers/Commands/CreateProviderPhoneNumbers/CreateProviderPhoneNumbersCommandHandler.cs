using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumbers
{
    public class CreateProviderPhoneNumbersCommandHandler : IRequestHandler
        <CreateProviderPhoneNumbersCommand, List<Guid>>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateProviderPhoneNumbersCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<List<Guid>> Handle(CreateProviderPhoneNumbersCommand request,
            CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var result = new List<Guid>();
            foreach (var phoneNumber in request.PhoneNumbers)
            {
                var providerPhoneNumber = new ProviderPhoneNumber
                {
                    Id          = Guid.NewGuid(),
                    ProviderId  = provider.Id,
                    PhoneNumber = phoneNumber,
                };

                await _dbContext.ProviderPhoneNumbers.AddAsync(providerPhoneNumber,
                    cancellationToken);

                result.Add(providerPhoneNumber.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return result; 
        }
    }
}
