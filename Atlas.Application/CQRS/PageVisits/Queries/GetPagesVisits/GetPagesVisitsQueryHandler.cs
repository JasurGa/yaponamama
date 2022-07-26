using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PageVisits.Queries.GetPagesVisits
{
    public class GetPagesVisitsQueryHandler : IRequestHandler<GetPagesVisitsQuery,
        IList<int>>
    {
        private readonly IAtlasDbContext _dbContext;

        public GetPagesVisitsQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IList<int>> Handle(GetPagesVisitsQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<int>();
            foreach (var page in request.Pages)
            {
                var pageVisit = await _dbContext.PageVisits.FirstOrDefaultAsync(x =>
                    x.Path == page, cancellationToken);

                result.Add(pageVisit != null ? pageVisit.VisitedCount : 0);
            }

            return result;
        }
    }
}
