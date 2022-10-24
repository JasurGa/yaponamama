using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests;
using Atlas.Application.Models;
using AutoMapper;
using Atlas.Application.Interfaces;
using MediatR;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetPagedVerificationRequestsList
{
    public class GetPagedVerificationRequestsListQueryHandler : IRequestHandler<GetPagedVerificationRequestsListQuery,
        PageDto<VerificationRequestLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPagedVerificationRequestsListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<VerificationRequestLookupDto>> Handle(GetPagedVerificationRequestsListQuery request,
            CancellationToken cancellationToken)
        {
            var verificationRequestsCount = await _dbContext.VerificationRequests
                .CountAsync(cancellationToken);

            var verificationRequests = await _dbContext.VerificationRequests.OrderByDescending(x => x.SendAt)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<VerificationRequestLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<VerificationRequestLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = verificationRequestsCount,
                PageCount  = (int)Math.Ceiling((double)verificationRequestsCount / request.PageSize),
                Data       = verificationRequests,
            };
        }
    }
}

