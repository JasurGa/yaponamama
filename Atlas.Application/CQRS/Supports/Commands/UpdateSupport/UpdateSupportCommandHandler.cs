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
        private readonly IAtlasDbContext _dbContext;

        public UpdateSupportCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateSupportCommand request, CancellationToken cancellationToken)
        {
            var support = await _dbContext.Supports.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (support == null)
            {
                throw new NotFoundException(nameof(Support), request.Id);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            support.UserId = user.Id;
            support.PassportPhotoPath = request.PassportPhotoPath;
            support.InternalPhoneNumber = request.InternalPhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
