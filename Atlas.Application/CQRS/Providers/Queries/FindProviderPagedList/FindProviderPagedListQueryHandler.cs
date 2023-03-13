using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Providers.Queries.FindProviderPagedList
{
    public class FindProviderPagedListQueryHandler : IRequestHandler<FindProviderPagedListQuery,
        PageDto<ProviderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindProviderPagedListQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ProviderLookupDto>> Handle(FindProviderPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var providers = _dbContext.Providers.Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderBy(x => EF.Functions.TrigramsSimilarity((x.Name + " " + x.Description).ToLower().Trim(),
                    request.SearchQuery));

            var providersCount = await providers.CountAsync(cancellationToken);
            var pagedProviders = await providers.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<ProviderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = providersCount,
                PageCount  = (int)Math.Ceiling((double)providersCount / request.PageSize),
                Data       = _mapper.Map<List<Provider>, List<ProviderLookupDto>>(pagedProviders),
            };
        }
    }
}

