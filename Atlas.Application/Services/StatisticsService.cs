using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using Coravel.Invocable;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace Atlas.Application.Services
{
    public class StatisticsService : IInvocable
    {
        private readonly IAtlasDbContext _dbContext;

        public StatisticsService(IAtlasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Invoke()
        {
            var debit  = _dbContext.Consignments.Select(x => x.Count * x.CurrentPurchasePrice).Sum();
            var credit = ((long)_dbContext.Orders.Select(x => x.SellingPrice).Sum());

            await _dbContext.DebitCreditStatistics.AddAsync(new DebitCreditStatistics
            {
                Id      = Guid.NewGuid(),
                Credit  = credit,
                Debit   = debit,
                AddedAt = DateTime.UtcNow,
            });
        }
    }
}

