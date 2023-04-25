using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodWithDiscountPagedList
{
    public class GetGoodWithDiscountPagedListQueryHandler : IRequestHandler<GetGoodWithDiscountPagedListQuery,
        PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodWithDiscountPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(GetGoodWithDiscountPagedListQuery request, 
            CancellationToken cancellationToken)
        {
            var goodsCount = await _dbContext.Goods
                .Where(x => x.Discount > 0 && x.IsDeleted == request.ShowDeleted)
                .CountAsync(cancellationToken);

            var goods = await _dbContext.Goods
                .Where(x => x.Discount > 0 && x.IsDeleted == request.ShowDeleted)
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = goodsCount,
                PageCount  = (int)Math.Ceiling((double)goodsCount / request.PageSize),
                Data       = goods
            };
        }
    }
}
