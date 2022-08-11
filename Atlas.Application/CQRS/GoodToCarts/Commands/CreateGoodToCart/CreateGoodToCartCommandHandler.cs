using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.CreateGoodToCart
{
    public class CreateGoodToCartCommandHandler : IRequestHandler<CreateGoodToCartCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateGoodToCartCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateGoodToCartCommand request, CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x => 
                x.Id == request.GoodId, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.GoodId);
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => 
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var goodToCart = new GoodToCart
            {
                Id       = Guid.NewGuid(),
                GoodId   = good.Id,
                ClientId = client.Id,
                Count    = request.Count,
            };

            await _dbContext.GoodToCarts.AddAsync(goodToCart, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return goodToCart.Id;
        }
    }
}
