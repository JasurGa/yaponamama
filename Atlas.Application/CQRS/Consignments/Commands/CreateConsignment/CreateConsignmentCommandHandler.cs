using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommandHandler : IRequestHandler<CreateConsignmentCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateConsignmentCommand request,
            CancellationToken cancellationToken)
        {
            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.Id == request.StoreToGoodId, cancellationToken);

            if (storeToGood == null)
            {
                throw new NotFoundException(nameof(StoreToGood), request.StoreToGoodId);
            }

            var consignment = new Consignment
            {
                Id              = Guid.NewGuid(),
                ExpirateAt      = request.ExpirateAt,
                PurchasedAt     = request.PurchasedAt,
                ShelfLocation   = request.ShelfLocation,
                StoreToGoodId   = request.StoreToGoodId,
                Count           = request.Count
            };

            storeToGood.Count += request.Count;

            await _dbContext.Consignments.AddAsync(consignment,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return consignment.Id;
        }
    }
}
