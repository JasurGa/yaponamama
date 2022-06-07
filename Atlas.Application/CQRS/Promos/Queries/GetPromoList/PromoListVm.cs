using System.Collections.Generic;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoList
{
    public class PromoListVm
    {
        public IList<PromoLookupDto> Promos { get; set; }
    }
}
