using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodCounts
{
    public class GetGoodCountsQueryHandler : IRequestHandler
        <GetGoodCountsQuery, int>
    {
        private readonly IAtlasDbContext _dbContext;

        public GetGoodCountsQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<int> Handle(GetGoodCountsQuery request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x =>
                x.Id == request.CategoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }

            var count = await _dbContext.CategoryToGoods.CountAsync(x =>
                x.CategoryId == request.CategoryId);

            return count;
        }
    }
}
