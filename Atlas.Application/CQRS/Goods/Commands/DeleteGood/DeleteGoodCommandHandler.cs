using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Commands.DeleteGood
{
    public class DeleteGoodCommandHandler : IRequestHandler<DeleteGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteGoodCommand request, CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.Id);
            }

            good.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
