using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetAvailableGoodList
{
    public class GetAvailableGoodListQueryHandler : IRequestHandler<GetAvailableGoodListQuery, List<Guid>>
    {
        private readonly IAtlasDbContext _dbContext;

        public GetAvailableGoodListQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<List<Guid>> Handle(GetAvailableGoodListQuery request, CancellationToken cancellationToken)
        {
            var goodIds = await _dbContext.Goods
                .Where(x => x.StoreToGoods.Sum(x => x.Count) > 0)
                .OrderByDescending(x => x.StoreToGoods.Select(x => x.Count).Sum())
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            return goodIds;
        }
    }
}
