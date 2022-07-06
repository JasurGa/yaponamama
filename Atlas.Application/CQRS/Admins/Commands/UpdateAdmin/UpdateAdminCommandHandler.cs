using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Admins.Commands.UpdateAdmin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateAdminCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (admin == null)
            {
                throw new NotFoundException(nameof(Admin), admin.Id);
            }

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

            admin.KPI                 = request.KPI;
            admin.UserId              = request.UserId;
            admin.OfficialRoleId      = request.OfficialRoleId;
            admin.WorkingDayDuration  = request.WorkingDayDuration;
            admin.StartOfWorkingHours = request.StartOfWorkingHours;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
