using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GetGoodListByCategoryQueryHandler : IRequestHandler<GetGoodListByCategoryQuery,
        GoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodListByCategoryQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GoodListVm> Handle(GetGoodListByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var goodIds = await _dbContext.CategoryToGoods
                .Where(x => x.CategoryId == request.CategoryId)
                .Select(e => e.GoodId)
                .ToListAsync(cancellationToken);

            var goods = await _dbContext.Goods
                .Where(e => goodIds.Contains(e.Id))
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm { Goods = goods };
        }
    }
}
