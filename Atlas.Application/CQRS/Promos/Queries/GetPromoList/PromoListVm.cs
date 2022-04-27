using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoList
{
    public class PromoListVm
    {
        public IList<PromoLookupDto> Promos { get; set; }
    }
}
