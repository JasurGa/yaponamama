using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategoryById
{
    public class GetGeneralCategoryByIdQueryHandler : IRequestHandler
        <GetGeneralCategoryByIdQuery, GeneralCategoryLookupDto>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGeneralCategoryByIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GeneralCategoryLookupDto> Handle(GetGeneralCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            return _mapper.Map<GeneralCategory, GeneralCategoryLookupDto>
                (await _dbContext.GeneralCategories
                    .FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken));
        }
    }
}
