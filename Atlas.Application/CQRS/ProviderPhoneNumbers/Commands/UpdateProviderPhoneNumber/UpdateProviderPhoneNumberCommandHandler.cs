using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.UpdateProviderPhoneNumber
{
    public class UpdateProviderPhoneNumberCommandHandler : IRequestHandler<UpdateProviderPhoneNumberCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateProviderPhoneNumberCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProviderPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var providerPhoneNumber = await _dbContext.ProviderPhoneNumbers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (providerPhoneNumber == null)
            {
                throw new NotFoundException(nameof(ProviderPhoneNumber), request.Id);
            }

            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            providerPhoneNumber.ProviderId  = provider.Id;
            providerPhoneNumber.PhoneNumber = request.PhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
