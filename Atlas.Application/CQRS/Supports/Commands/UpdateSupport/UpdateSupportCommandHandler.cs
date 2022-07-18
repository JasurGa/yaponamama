using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Supports.Commands.UpdateSupport
{
    public class UpdateSupportCommandHandler : IRequestHandler<UpdateSupportCommand>
    {
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public UpdateSupportCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Unit> Handle(UpdateSupportCommand request, CancellationToken cancellationToken)
        {
            var support = await _dbContext.Supports.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (support == null)
            {
                throw new NotFoundException(nameof(Support), request.Id);
            }

            request.User.Id = support.UserId;
            await _mediator.Send(request.User);

            support.PassportPhotoPath   = request.PassportPhotoPath;
            support.InternalPhoneNumber = request.InternalPhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
