using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Supports.Commands.CreateSupport
{
    public class CreateSupportCommandHandler : IRequestHandler<CreateSupportCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateSupportCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateSupportCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var support = new Support
            {
                Id                  = Guid.NewGuid(),
                UserId              = user.Id,
                InternalPhoneNumber = request.InternalPhoneNumber,
                PassportPhotoPath   = request.PassportPhotoPath,
                IsDeleted           = false
            };

            await _dbContext.Supports.AddAsync(support, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return support.Id;
        }
    }
}
