using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByProvider
{
    public class GetGoodPagedListByProviderQueryHandler : IRequestHandler<GetGoodPagedListByProviderQuery, PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodPagedListByProviderQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(GetGoodPagedListByProviderQuery request, CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x => 
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var goodsCount = await _dbContext.Goods
                .Where(x => x.IsDeleted == request.ShowDeleted && x.ProviderId == request.ProviderId)
                .CountAsync(cancellationToken);

            var goods = await _dbContext.Goods
                .Where(x => x.IsDeleted == request.ShowDeleted && x.ProviderId == request.ProviderId)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = goodsCount,
                PageCount  = (int)Math.Ceiling((double)goodsCount / request.PageSize),
                Data       = goods,
            };
        }
    }
}
