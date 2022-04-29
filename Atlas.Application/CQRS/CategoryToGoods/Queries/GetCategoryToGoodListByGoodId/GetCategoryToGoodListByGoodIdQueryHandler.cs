using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class GetCategoryToGoodListByGoodIdQueryHandler : IRequestHandler<GetCategoryToGoodListByGoodIdQuery, CategoryToGoodListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCategoryToGoodListByGoodIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CategoryToGoodListVm> Handle(GetCategoryToGoodListByGoodIdQuery request, CancellationToken cancellationToken)
        {
            var categoryToGoods = await _dbContext.CategoryToGoods
                .Where(ctg => ctg.GoodId == request.GoodId)
                .ProjectTo<CategoryToGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CategoryToGoodListVm{ CategoryToGoods = categoryToGoods };
        }
    }
}
