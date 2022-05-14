using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Languages.Queries
{
    public class GetLanguageListQueryHandler : IRequestHandler<GetLanguageListQuery, LanguageListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetLanguageListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<LanguageListVm> Handle(GetLanguageListQuery request, CancellationToken cancellationToken)
        {
            var languages = await _dbContext.Languages
                .ProjectTo<LanguageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new LanguageListVm { Languages = languages };
        }
    }
}
