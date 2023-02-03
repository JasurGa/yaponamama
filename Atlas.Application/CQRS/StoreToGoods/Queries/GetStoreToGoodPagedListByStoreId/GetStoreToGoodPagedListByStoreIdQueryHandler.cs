using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Extensions;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId
{
    public class GetStoreToGoodPagedListByStoreIdQueryHandler : IRequestHandler<GetStoreToGoodPagedListByStoreIdQuery, PageDto<StoreToGoodLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetStoreToGoodPagedListByStoreIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<StoreToGoodLookupDto>> Handle(GetStoreToGoodPagedListByStoreIdQuery request, CancellationToken cancellationToken)
        {
            var storeToGoodsQuery = _dbContext.StoreToGoods
                .Where(x => x.StoreId == request.StoreId && x.Good.IsDeleted == false);

            //if (request.IgnoreNulls)
            //{
                storeToGoodsQuery = storeToGoodsQuery
                    .Where(x => x.Count != 0);
            //}

            var storeToGoodsCount = await storeToGoodsQuery.CountAsync(x =>
                x.StoreId == request.StoreId,
                    cancellationToken);

            var storeToGoods = await storeToGoodsQuery
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<StoreToGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<StoreToGoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = storeToGoodsCount,
                PageCount  = (int)Math.Ceiling((double)storeToGoodsCount / request.PageSize),
                Data       = storeToGoods
            };
        }
    }
}
