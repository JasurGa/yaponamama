using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId;
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

namespace Atlas.Application.CQRS.StoreToGoods.Queries.FindStoreToGoodPagedList
{
    public class FindStoreToGoodPagedListQueryHandler : IRequestHandler<FindStoreToGoodPagedListQuery, PageDto<StoreToGoodLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindStoreToGoodPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<StoreToGoodLookupDto>> Handle(FindStoreToGoodPagedListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.StoreToGoods.Where(x => 
                x.StoreId == request.StoreId).AsQueryable();

            if (request.SearchQuery != null)
            {
                var notTranslited = request.SearchQuery.ToLower().Trim();

                query = query
                    .Where(x => EF.Functions.TrigramsSimilarity(
                        (x.Good.NameRu + " " + x.Good.NameEn + " " + x.Good.NameUz + " " +
                        x.Good.DescriptionRu + " " + x.Good.DescriptionEn + " " + x.Good.DescriptionUz + " " +
                        x.Good.SellingPrice + " " + x.Good.PackageCode).ToLower().Trim(),
                        notTranslited) > 0)
                    .OrderByDescending(x => EF.Functions.TrigramsSimilarity(
                        (x.Good.NameRu + " " + x.Good.NameEn + " " + x.Good.NameUz + " " +
                        x.Good.DescriptionRu + " " + x.Good.DescriptionEn + " " + x.Good.DescriptionUz + " " +
                        x.Good.SellingPrice + " " + x.Good.PackageCode).ToLower().Trim(),
                        notTranslited));
            } 
            else
            {
                query = query.OrderByDescending(x => x.Count);
            }

            var goodsCount = await query.CountAsync(cancellationToken);
            var goods = await query
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<StoreToGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<StoreToGoodLookupDto>
            {
                PageIndex   = request.PageIndex,
                TotalCount  = goodsCount,
                PageCount   = (int)Math.Ceiling((double)goodsCount / request.PageSize),
                Data        = goods,
            };
        }
    }
}
