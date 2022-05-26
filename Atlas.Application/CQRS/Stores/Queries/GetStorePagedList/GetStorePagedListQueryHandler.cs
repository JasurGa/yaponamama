using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Stores.Queries.GetStorePagedList
{
    public class GetStorePagedListQueryHandler : IRequestHandler<GetStorePagedListQuery, PageDto<StoreLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetStorePagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<StoreLookupDto>> Handle(GetStorePagedListQuery request, CancellationToken cancellationToken)
        {
            var storesCount = await _dbContext.Stores.CountAsync(x =>
                x.IsDeleted == request.ShowDeleted, cancellationToken);
                
            var stores = await _dbContext.Stores
                .Where(s => s.IsDeleted == request.ShowDeleted)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<StoreLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<StoreLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = storesCount,
                PageCount  = (int)Math.Ceiling((double)storesCount / request.PageSize),
                Data       = stores
            };
        }
    }
}
