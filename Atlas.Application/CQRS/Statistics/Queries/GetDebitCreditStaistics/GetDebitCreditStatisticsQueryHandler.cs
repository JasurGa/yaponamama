using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Statistics.Queries.GetDebitCreditStaistics
{
    public class GetDebitCreditStatisticsQueryHandler : IRequestHandler<GetDebitCreditStatisticsQuery, DebitCreditLookupDto>
    {
        private readonly IAtlasDbContext _dbContext;

        public GetDebitCreditStatisticsQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<DebitCreditLookupDto> Handle(GetDebitCreditStatisticsQuery request, CancellationToken cancellationToken)
        {
            var lastAdded = await _dbContext.DebitCreditStatistics.OrderByDescending(x => x.AddedAt)
                .FirstOrDefaultAsync(cancellationToken);

            return new DebitCreditLookupDto
            {
                Credit = lastAdded.Credit,
                Debit  = lastAdded.Debit,
            };
        }
    }
}

