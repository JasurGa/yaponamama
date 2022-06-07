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

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList
{
    public class GetSupplyManagerPagedListQueryHandler : IRequestHandler<GetSupplyManagerPagedListQuery, PageDto<SupplyManagerLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetSupplyManagerPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<SupplyManagerLookupDto>> Handle(GetSupplyManagerPagedListQuery request, CancellationToken cancellationToken)
        {
            var supplyManagersCount = await _dbContext.SupplyManagers.CountAsync(x =>
                x.IsDeleted == request.ShowDeleted, cancellationToken);

            var supplyManagers = await _dbContext.SupplyManagers
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Include(x => x.User)
                .ProjectTo<SupplyManagerLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<SupplyManagerLookupDto>
            {
                PageIndex = request.PageIndex,
                TotalCount = supplyManagersCount,
                PageCount = (int)Math.Ceiling((double)supplyManagersCount / request.PageSize),
                Data = supplyManagers,
            };
        }
    }
}
