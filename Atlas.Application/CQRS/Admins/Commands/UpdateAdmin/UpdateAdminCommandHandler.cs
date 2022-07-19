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
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public UpdateAdminCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (admin == null)
            {
                throw new NotFoundException(nameof(Admin), admin.Id);
            }

            request.User.Id = admin.UserId;
            await _mediator.Send(request.User);

            var officialRole = await _dbContext.OfficialRoles.FirstOrDefaultAsync(x =>
                x.Id == request.OfficialRoleId, cancellationToken);

            if (officialRole == null)
            {
                throw new NotFoundException(nameof(OfficialRole), request.OfficialRoleId);
            }

            admin.PhoneNumber         = request.PhoneNumber;
            admin.KPI                 = request.KPI;
            admin.OfficialRoleId      = request.OfficialRoleId;
            admin.WorkingDayDuration  = request.WorkingDayDuration;
            admin.StartOfWorkingHours = request.StartOfWorkingHours;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
