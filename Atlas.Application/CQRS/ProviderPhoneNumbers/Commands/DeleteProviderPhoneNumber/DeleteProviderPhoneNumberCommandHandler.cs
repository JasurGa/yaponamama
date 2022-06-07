using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.DeleteProviderPhoneNumber
{
    public class DeleteProviderPhoneNumberCommandHandler : IRequestHandler<DeleteProviderPhoneNumberCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteProviderPhoneNumberCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteProviderPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var providerPhoneNumber = await _dbContext.ProviderPhoneNumbers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (providerPhoneNumber == null)
            {
                throw new NotFoundException(nameof(ProviderPhoneNumber), request.Id);
            }

            _dbContext.ProviderPhoneNumbers.Remove(providerPhoneNumber);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
