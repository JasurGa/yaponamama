using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.DeclineVerificationRequest
{
    public class DeclineVerificationRequestCommandHandler : IRequestHandler
        <DeclineVerificationRequestCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeclineVerificationRequestCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeclineVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var verificationRequest = await _dbContext.VerificationRequests.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (verificationRequest == null)
            {
                throw new NotFoundException(nameof(VerificationRequest), request.Id);
            }

            verificationRequest.IsChecked  = true;
            verificationRequest.IsVerified = false;
            verificationRequest.Comment    = request.Comment;


            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == verificationRequest.ClientId, cancellationToken);

            client.IsPassportPending = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

