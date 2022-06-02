using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoriesByGeneralCategory
{
    public class GetCategoriesByGeneralCategoryQueryHandler : IRequestHandler
        <GetCategoriesByGeneralCategoryQuery, CategoryListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCategoriesByGeneralCategoryQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CategoryListVm> Handle(GetCategoriesByGeneralCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories
                .Where(x => x.GeneralCategoryId == request.GeneralCategoryId &&
                    x.IsDeleted == request.ShowDeleted)
                .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CategoryListVm
            {
                Categories = categories
            };
        }
    }
}
