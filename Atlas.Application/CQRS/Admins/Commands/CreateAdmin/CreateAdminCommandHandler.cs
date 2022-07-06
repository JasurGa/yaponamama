using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateAdminCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null || user.IsDeleted)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var officialRole = await _dbContext.OfficialRoles.FirstOrDefaultAsync(x =>
                x.Id == request.OfficialRoleId, cancellationToken);

            if (officialRole == null)
            {
                throw new NotFoundException(nameof(OfficialRole), request.OfficialRoleId);
            }

            var entity = new Admin
            {
                Id                  = Guid.NewGuid(),
                KPI                 = 0,
                UserId              = request.UserId,
                OfficialRoleId      = request.OfficialRoleId,
                StartOfWorkingHours = request.StartOfWorkingHours,
                WorkingDayDuration  = request.WorkingDayDuration,
                IsDeleted           = false
            };

            await _dbContext.Admins.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
