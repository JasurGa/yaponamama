using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Users.Commands.RestoreUser
{
    public class RestoreUserCommandHandler : IRequestHandler<RestoreUserCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreUserCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (user == null || !user.IsDeleted)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            user.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
