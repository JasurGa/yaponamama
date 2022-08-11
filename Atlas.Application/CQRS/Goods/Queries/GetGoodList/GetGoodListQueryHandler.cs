using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodList
{
    public class GetGoodListQueryHandler : IRequestHandler<GetGoodListQuery, GoodListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;
        public GetGoodListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GoodListVm> Handle(GetGoodListQuery request, CancellationToken cancellationToken)
        {
            var goods = await _dbContext.Goods
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm { Goods = goods };
        }
    }
}
