using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Atlas.Domain;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateUserCommandHandler(IAtlasDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u =>u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            user.FirstName       = request.FirstName;
            user.LastName        = request.LastName;
            user.Birthday        = request.Birthday;
            user.AvatarPhotoPath = request.AvatarPhotoPath;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
