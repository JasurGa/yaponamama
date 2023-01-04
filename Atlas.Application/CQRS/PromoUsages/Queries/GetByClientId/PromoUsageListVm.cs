using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetByClientId
{
    public class PromoUsageListVm
    {
        public ICollection<PromoUsageLookupDto> PromoUsages { get; set; }
    }
}