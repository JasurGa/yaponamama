using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.FindSupplyManagerPagedList
{
    public class FindSupplyManagerPagedListQueryHandler : IRequestHandler<FindSupplyManagerPagedListQuery,
        PageDto<SupplyManagerLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindSupplyManagerPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<SupplyManagerLookupDto>> Handle(FindSupplyManagerPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var supplyManagers = _dbContext.SupplyManagers.Include(x => x.User).Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderBy(x => EF.Functions.TrigramsSimilarity((x.PhoneNumber + " " + x.User.Login + " " + x.User.FirstName + " " + x.User.LastName + " " + x.User.MiddleName).ToLower().Trim(),
                    request.SearchQuery));

            var supplyManagersCount = await supplyManagers.CountAsync(cancellationToken);
            var pagedSupplyManagers = await supplyManagers.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<SupplyManagerLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = supplyManagersCount,
                PageCount  = (int)Math.Ceiling((double)supplyManagersCount / request.PageSize),
                Data       = _mapper.Map<List<SupplyManager>, List<SupplyManagerLookupDto>>(pagedSupplyManagers),
            };
        }
    }
}

