using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryPagedList
{
    public class GetPromoCategoryPagedListQueryHandler : IRequestHandler<GetPromoCategoryPagedListQuery,
        PageDto<PromoCategoryLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoCategoryPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<PromoCategoryLookupDto>> Handle(GetPromoCategoryPagedListQuery request, CancellationToken cancellationToken)
        {
            var promoCategoriesCount = await _dbContext.PromoCategories
                .CountAsync(cancellationToken);

            var promoCategories = await _dbContext.PromoCategories
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .Take(request.PageSize)
                .Skip(request.PageSize * request.PageIndex)
                .ProjectTo<PromoCategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<PromoCategoryLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = promoCategoriesCount,
                PageCount  = (int)Math.Ceiling((double)promoCategoriesCount / request.PageSize),
                Data       = promoCategories
            };
        }
    }
}

