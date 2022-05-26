using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Commands.RestoreGood
{
    public class RestoreGoodCommandHandler : IRequestHandler<RestoreGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreGoodCommand request, CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (good == null || !good.IsDeleted)
            {
                throw new NotFoundException(nameof(Good), request.Id);
            }

            good.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
