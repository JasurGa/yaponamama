using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Statistics.Queries.GetOverallBalanceOfClients
{
    public class GetOverallBallanceOfClientsQueryHandler : IRequestHandler<GetOverallBalanceOfClientsQuery, long>
    {
        private readonly IAtlasDbContext _dbContext;
        public GetOverallBallanceOfClientsQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<long> Handle(GetOverallBalanceOfClientsQuery request, CancellationToken cancellationToken)
        {
            var overallBalance = await _dbContext.Clients.SumAsync(c => c.Balance, cancellationToken);

            return overallBalance;

        }
    }
}
