using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId
{
    public class PromoUsageListVm
    {
        public ICollection<PromoUsageLookupDto> PromoUsages { get; set; }
    }
}