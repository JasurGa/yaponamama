using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood
{
    public class UpdateStoreToGoodCommandHandler : IRequestHandler<UpdateStoreToGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateStoreToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateStoreToGoodCommand request, CancellationToken cancellationToken)
        {
            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (storeToGood == null)
            {
                throw new NotFoundException(nameof(StoreToGood), request.Id);
            }

            storeToGood.Count = request.Count;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
