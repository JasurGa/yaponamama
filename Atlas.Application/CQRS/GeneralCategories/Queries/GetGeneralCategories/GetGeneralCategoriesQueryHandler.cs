using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories
{
    public class GetGeneralCategoriesQueryHandler : IRequestHandler
        <GetGeneralCategoriesQuery, GeneralCategoriesListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGeneralCategoriesQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GeneralCategoriesListVm> Handle(GetGeneralCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var generalCategories = await _dbContext.GeneralCategories
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .ProjectTo<GeneralCategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GeneralCategoriesListVm
            {
                GeneralCategories = generalCategories
            };
        }
    }
}
