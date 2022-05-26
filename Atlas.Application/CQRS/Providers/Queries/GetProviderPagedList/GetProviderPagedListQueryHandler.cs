using Atlas.Application.CQRS.Providers.Queries.GetProviderList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
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

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList
{
    public class GetProviderPagedListQueryHandler : IRequestHandler<GetProviderPagedListQuery, PageDto<ProviderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ProviderLookupDto>> Handle(GetProviderPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var providersCount = await _dbContext.Providers.CountAsync(cancellationToken);

            var providers = await _dbContext.Providers
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProviderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<ProviderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = providersCount,
                PageCount  = (int)Math.Ceiling((double)providersCount / request.PageSize),
                Data       = providers
            };
        }
    }
}
