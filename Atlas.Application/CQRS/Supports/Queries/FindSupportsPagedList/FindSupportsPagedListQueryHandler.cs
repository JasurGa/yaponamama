using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Supports.Queries.FindSupportsPagedList
{
    public class FindSupportsPagedListQueryHandler : IRequestHandler<FindSupportsPagedListQuery,
        PageDto<SupportLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindSupportsPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<SupportLookupDto>> Handle(FindSupportsPagedListQuery request, CancellationToken cancellationToken)
        {
            var supports = _dbContext.Supports.Include(x => x.User)
                .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance($"{x.InternalPhoneNumber} {x.User.Login} {x.User.FirstName} {x.User.LastName} {x.User.MiddleName}",
                    request.SearchQuery));

            var supportsCount = await supports.CountAsync(cancellationToken);
            var pagedSupports = await supports.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<SupportLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = supportsCount,
                PageCount  = (int)Math.Ceiling((double)supportsCount / request.PageSize),
                Data       = _mapper.Map<List<Support>, List<SupportLookupDto>>(pagedSupports),
            };            
        }
    }
}

