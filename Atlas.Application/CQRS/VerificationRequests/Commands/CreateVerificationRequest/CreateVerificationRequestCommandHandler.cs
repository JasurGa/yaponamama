using System;
using MediatR;
using Atlas.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.CreateVerificationRequest
{
    public class CreateVerificationRequestCommandHandler : IRequestHandler
        <CreateVerificationRequestCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateVerificationRequestCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var verificationRequest = new VerificationRequest
            {
                Id                          = Guid.NewGuid(),
                ClientId                    = request.ClientId,
                PassportPhotoPath           = request.PassportPhotoPath,
                SelfieWithPassportPhotoPath = request.SelfieWithPassportPhotoPath,
                IsVerified                  = false,
                IsChecked                   = false,
                SendAt                      = DateTime.UtcNow,
                Comment                     = "",
            };

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            client.IsPassportPending = true;

            await _dbContext.VerificationRequests.AddAsync(verificationRequest,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return verificationRequest.Id;
        }
    }
}