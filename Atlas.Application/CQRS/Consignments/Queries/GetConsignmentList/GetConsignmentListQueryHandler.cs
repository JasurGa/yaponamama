using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class GetConsignmentListQueryHandler : IRequestHandler<GetConsignmentListQuery, ConsignmentListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetConsignmentListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ConsignmentListVm> Handle(GetConsignmentListQuery request, CancellationToken cancellationToken)
        {
            var consignments = await _dbContext.Consignments
                .ProjectTo<ConsignmentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ConsignmentListVm { Consignments = consignments };
        }
    }
}
