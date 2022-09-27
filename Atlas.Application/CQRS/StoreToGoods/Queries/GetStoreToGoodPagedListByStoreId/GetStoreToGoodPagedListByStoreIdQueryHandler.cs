﻿using System;
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
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var storeToGoods = _dbContext.StoreToGoods.AsQueryable();

            storeToGoods = storeToGoods.Where(x => x.StoreId == request.StoreId)
                .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance((x.Good.Name + " " + x.Good.NameRu + " " + x.Good.NameEn + " " + x.Good.NameUz + " " + x.Good.SellingPrice.ToString()).ToLower().Trim(),
                    request.SearchQuery));

            var storeToGoodsCount = await storeToGoods.CountAsync(cancellationToken);
            var pagedStoreToGoods = await storeToGoods
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<StoreToGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            //var storeToGoods = await _dbContext.StoreToGoods
            //    .Where(x => x.StoreId == request.StoreId)
            //    .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance((x.Good.Name + " " + x.Good.NameRu + " " + x.Good.NameEn + " " + x.Good.NameUz + " " + x.Good.SellingPrice.ToString()).ToLower().Trim(),
            //        request.SearchQuery.ToLower().Trim()))
            //    .Skip(request.PageIndex * request.PageSize)
            //    .Take(request.PageSize)
            //    .ProjectTo<StoreToGoodLookupDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync(cancellationToken);

            return new PageDto<StoreToGoodLookupDto>
            {
                PageIndex = request.PageIndex,
                TotalCount = storeToGoodsCount,
                PageCount = (int)Math.Ceiling((double)storeToGoodsCount / request.PageSize),
                Data = pagedStoreToGoods
            };
        }
    }
}
