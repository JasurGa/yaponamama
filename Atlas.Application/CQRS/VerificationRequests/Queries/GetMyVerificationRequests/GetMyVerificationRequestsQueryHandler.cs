using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests
{
    public class GetMyVerificationRequestsQueryHandler : IRequestHandler<GetMyVerificationRequestsQuery,
        PageDto<VerificationRequestLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetMyVerificationRequestsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<VerificationRequestLookupDto>> Handle(GetMyVerificationRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var verificationRequestsCount = await _dbContext.VerificationRequests
                .Where(x => x.ClientId == request.ClientId)
                .CountAsync(cancellationToken);

            var verificationRequests = await _dbContext.VerificationRequests.OrderBy(x => x.SendAt)
                .Where(x => x.ClientId == request.ClientId)
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

