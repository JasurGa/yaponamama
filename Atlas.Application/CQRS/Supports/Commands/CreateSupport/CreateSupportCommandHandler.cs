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
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public CreateSupportCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Guid> Handle(CreateSupportCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(request.User);

            var support = new Support
            {
                Id                  = Guid.NewGuid(),
                UserId              = userId,
                StartOfWorkingHours = request.StartOfWorkingHours,
                WorkingDayDuration  = request.WorkingDayDuration,
                Salary              = request.Salary,
                KPI                 = 0,
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
