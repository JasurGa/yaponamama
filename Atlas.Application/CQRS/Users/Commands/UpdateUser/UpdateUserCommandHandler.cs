using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Atlas.Domain;
using Atlas.Application.Common.Helpers;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateUserCommandHandler(IAtlasDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var userWithTheSameLogin = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id != request.Id && x.Login == request.Login, cancellationToken);

            if (userWithTheSameLogin != null)
            {
                throw new AlreadyExistsException(nameof(User), request.Login);
            }

            user.Login           = request.Login;
            user.FirstName       = request.FirstName;
            user.LastName        = request.LastName;
            user.MiddleName      = request.MiddleName;
            user.Birthday        = request.Birthday;
            user.AvatarPhotoPath = request.AvatarPhotoPath;

            if (request.Password != null)
                user.PasswordHash = Sha256Crypto.GetHash(user.Salt + request.Password);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
