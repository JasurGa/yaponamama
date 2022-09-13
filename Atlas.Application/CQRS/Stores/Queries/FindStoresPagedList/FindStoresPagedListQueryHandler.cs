using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Stores.Queries.FindStoresPagedList
{
    public class FindStoresPagedListQueryHandler : IRequestHandler<FindStoresPagedListQuery,
        PageDto<StoreLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindStoresPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<StoreLookupDto>> Handle(FindStoresPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var stores = _dbContext.Stores.Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance($"{x.Name} {x.NameRu} {x.NameEn} {x.NameUz}".ToLower().Trim(),
                    request.SearchQuery));

            var storesCount = await stores.CountAsync(cancellationToken);
            var pagedStores = await stores.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<StoreLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = storesCount,
                PageCount  = (int)Math.Ceiling((double)storesCount / request.PageSize),
                Data       = _mapper.Map<List<Store>, List<StoreLookupDto>>(pagedStores),
            };
        }
    }
}

