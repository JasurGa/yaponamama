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
        private readonly IAtlasDbContext _dbContext;

        public CreateSupplyManagerCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateSupplyManagerCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => 
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var supplyManager = new SupplyManager
            {
                Id                = Guid.NewGuid(),
                UserId            = user.Id,
                StoreId           = request.StoreId,
                PhoneNumber       = request.PhoneNumber,
                PassportPhotoPath = request.PassportPhotoPath,
                IsDeleted         = false,
            };

            await _dbContext.SupplyManagers.AddAsync(supplyManager, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return supplyManager.Id;
        }
    }
}
