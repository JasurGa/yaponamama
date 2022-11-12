using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise
{
    public class PromoAdvertisePageListVm
    {
        public ICollection<PromoAdvertisePageLookupDto> PromoAdvertisePages { get; set; }
    }
}