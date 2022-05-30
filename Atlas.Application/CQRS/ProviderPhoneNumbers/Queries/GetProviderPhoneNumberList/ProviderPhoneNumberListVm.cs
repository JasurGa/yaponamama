using System.Collections.Generic;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberList
{
    public class ProviderPhoneNumberListVm
    {
        public IList<ProviderPhoneNumberLookupDto> ProviderPhoneNumbers { get; set; }
    }
}
