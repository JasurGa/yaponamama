using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.HeadRecruiters.Quieries.FindHeadRecruitersPagedList
{
    public class FindHeadRecruitersPagedListQueryHandler : IRequestHandler<FindHeadRecruitersPagedListQuery,
        PageDto<HeadRecruiterLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindHeadRecruitersPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<HeadRecruiterLookupDto>> Handle(FindHeadRecruitersPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var headRecruiters = _dbContext.HeadRecruiters.Include(x => x.User).Where(x => x.IsDeleted == request.ShowDeleted)
                .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance($"{x.User.Login} {x.User.FirstName} {x.User.LastName} {x.User.MiddleName}".ToLower().Trim(),
                    request.SearchQuery));

            var headRecruitersCount = await headRecruiters.CountAsync(cancellationToken);
            var pagedHeadRecruiters = await headRecruiters.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<HeadRecruiterLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = headRecruitersCount,
                PageCount  = (int)Math.Ceiling((double)headRecruitersCount / request.PageSize),
                Data       = _mapper.Map<List<HeadRecruiter>, List<HeadRecruiterLookupDto>>(pagedHeadRecruiters),
            };
        }
    }
}

