using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.AcceptVerificationRequest
{
    public class AcceptVerificationRequestCommandHandler : IRequestHandler<AcceptVerificationRequestCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public AcceptVerificationRequestCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(AcceptVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var verificationRequest = await _dbContext.VerificationRequests.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (verificationRequest == null)
            {
                throw new NotFoundException(nameof(VerificationRequest), request.Id);
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == verificationRequest.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), verificationRequest.ClientId);
            }

            client.PassportPhotoPath           = verificationRequest.PassportPhotoPath;
            client.SelfieWithPassportPhotoPath = verificationRequest.SelfieWithPassportPhotoPath;
            client.IsPassportVerified          = true;
            client.IsPassportPending           = false;

            verificationRequest.IsChecked  = true;
            verificationRequest.IsVerified = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

