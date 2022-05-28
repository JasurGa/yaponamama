using System.Collections.Generic;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class ProviderListVm
    {
        public IList<ProviderLookupDto> Providers { get; set; }
    }
}
