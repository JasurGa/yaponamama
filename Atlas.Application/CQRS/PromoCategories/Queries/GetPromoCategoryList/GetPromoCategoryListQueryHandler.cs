using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList
{
    public class GetPromoCategoryListQueryHandler : IRequestHandler<GetPromoCategoryListQuery,
        PromoCategoryListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoCategoryListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoCategoryListVm> Handle(GetPromoCategoryListQuery request, CancellationToken cancellationToken)
        {
            var promoCategories = await _dbContext.PromoCategories
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .ProjectTo<PromoCategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PromoCategoryListVm
            {
                PromoCategories = promoCategories
            };
        }
    }
}

