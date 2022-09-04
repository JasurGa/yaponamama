using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public UpdateClientCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            request.User.Id = client.UserId;
            await _mediator.Send(request.User, cancellationToken);

            client.UserId                      = request.User.Id;
            client.PassportPhotoPath           = request.PassportPhotoPath;
            client.SelfieWithPassportPhotoPath = request.SelfieWithPassportPhotoPath;
            client.IsPassportVerified          = request.IsPassportVerified;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
