using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList
{
    public class GetCategoryPagedListQueryHandler : IRequestHandler<GetCategoryPagedListQuery, PageDto<CategoryLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCategoryPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<CategoryLookupDto>> Handle(GetCategoryPagedListQuery request, CancellationToken cancellationToken)
        {
            var categoriesCount = await _dbContext.Categories
                .Where(c => c.IsDeleted == request.ShowDeleted)
                .CountAsync(cancellationToken);

            var categories = await _dbContext.Categories
                .Where(c => c.IsDeleted == request.ShowDeleted)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<CategoryLookupDto>
            {
                PageIndex = request.PageIndex,
                TotalCount = categoriesCount,
                PageCount = (int)Math.Ceiling((double)categoriesCount / request.PageSize),
                Data = categories
            };
        }
    }
}
