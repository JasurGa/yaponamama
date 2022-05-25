using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory
{
    public class GetGoodPagedListByCategoryQueryHandler : IRequestHandler<GetGoodPagedListByCategoryQuery,
        PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodPagedListByCategoryQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(GetGoodPagedListByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var goodIds = await _dbContext.CategoryToGoods
                .Where(x => x.CategoryId == request.CategoryId)
                .Select(e => e.GoodId).ToListAsync(cancellationToken);

            var goods = await _dbContext.Goods
                .Where(e => goodIds.Contains(e.Id))
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = goodIds.Count(),
                PageCount  = (int)Math.Ceiling((double)goodIds.Count() /
                    request.PageSize),
                Data = goods
            };
        }
    }
}
