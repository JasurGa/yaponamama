using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Application.Enums;
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
            var credit = _dbContext.Consignments.Where(x => !x.StoreToGood.Good.IsDeleted)
                .Select(x => x.Count * x.CurrentPurchasePrice).Sum();
            var debit = ((long)_dbContext.Orders.Where(x => !x.IsRefunded).Where(x => x.Status == (int)OrderStatus.Success)
                .Select(x => x.SellingPrice + x.ShippingPrice).Sum());

            await _dbContext.DebitCreditStatistics.AddAsync(new DebitCreditStatistics
            {
                Id      = Guid.NewGuid(),
                Credit  = credit,
                Debit   = debit,
                AddedAt = DateTime.UtcNow,
            });

            _dbContext.SaveChanges();
        }
    }
}

