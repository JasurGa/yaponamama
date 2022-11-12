using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises
{
    public class PromoAdvertisesListVm
    {
        public IEnumerable<PromoAdvertiseLookupDto> PromoAdvertises { get; set; }
    }
}

