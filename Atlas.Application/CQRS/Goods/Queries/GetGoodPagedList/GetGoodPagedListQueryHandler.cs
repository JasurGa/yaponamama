using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedList
{
    public class GetGoodPagedListQueryHandler : IRequestHandler<GetGoodPagedListQuery,
        PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(GetGoodPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var goodsCount = await _dbContext.Goods.CountAsync(x =>
                x.IsDeleted == request.ShowDeleted, cancellationToken);

            var goods = await _dbContext.Goods
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = goodsCount,
                PageCount  = (int)Math.Ceiling((double)goodsCount /
                    request.PageSize),
                Data       = goods
            };
            
        }

    }
}
