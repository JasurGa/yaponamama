using System.Collections.Generic;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId
{
    public class ProviderPhoneNumberListVm
    {
        public IList<ProviderPhoneNumberLookupDto> PhoneNumbers { get; set; }
    }
}
