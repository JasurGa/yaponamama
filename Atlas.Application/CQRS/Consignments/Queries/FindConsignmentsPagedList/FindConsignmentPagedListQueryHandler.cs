using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList
{
    public class FindConsignmentPagedListQueryHandler : IRequestHandler<FindConsignmentPagedListQuery,
        PageDto<ConsignmentLookupDto>>
    {
        private readonly IMapper         _mapper; 
        private readonly IAtlasDbContext _dbContext;

        public FindConsignmentPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ConsignmentLookupDto>> Handle(FindConsignmentPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var consigments = _dbContext.Consignments.Include(x => x.StoreToGood.Good)
                .OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance(
                    (x.StoreToGood.Good.Name + " " + x.StoreToGood.Good.NameRu + " " + x.StoreToGood.Good.NameEn + " " + x.StoreToGood.Good.NameUz).ToLower().Trim(),
                        request.SearchQuery));

            var consigmentsCount = await consigments.CountAsync(cancellationToken);
            var pagedConsigments = await consigments.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<ConsignmentLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = consigmentsCount,
                PageCount  = (int)Math.Ceiling((double)consigmentsCount / request.PageSize),
                Data       = _mapper.Map<List<Consignment>, List<ConsignmentLookupDto>>(pagedConsigments),
            };
        }
    }
}

