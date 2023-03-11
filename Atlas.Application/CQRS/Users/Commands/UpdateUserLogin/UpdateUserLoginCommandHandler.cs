using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandHandler : IRequestHandler<UpdateUserLoginCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateUserLoginCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => 
                x.Login == request.Login, cancellationToken);

            if (existingUser != null)
            {
                throw new ArgumentException("User with this login already exists");
            }

            var verification = await _dbContext.VerifyCodes.FirstOrDefaultAsync(x =>
                x.PhoneNumber == request.Login, cancellationToken);

            if (verification == null || !verification.IsVerified)
            {
                throw new NotFoundException(nameof(VerifyCode), request.Id);
            }

            user.Login = request.Login;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
