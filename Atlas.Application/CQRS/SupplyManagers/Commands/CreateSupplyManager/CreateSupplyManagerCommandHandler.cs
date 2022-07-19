using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager
{
    public class CreateSupplyManagerCommandHandler : IRequestHandler<CreateSupplyManagerCommand, Guid>
    {
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public CreateSupplyManagerCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Guid> Handle(CreateSupplyManagerCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(request.User);

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var supplyManager = new SupplyManager
            {
                Id                = Guid.NewGuid(),
                UserId            = userId,
                StoreId           = request.StoreId,
                PhoneNumber       = request.PhoneNumber,
                PassportPhotoPath = request.PassportPhotoPath,
                Salary            = request.Salary,
                IsDeleted         = false,
            };

            await _dbContext.SupplyManagers.AddAsync(supplyManager, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return supplyManager.Id;
        }
    }
}
