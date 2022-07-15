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
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public CreateAdminCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(request.User);

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
                UserId              = userId,
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
