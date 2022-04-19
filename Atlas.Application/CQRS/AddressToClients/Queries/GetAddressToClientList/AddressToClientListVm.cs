using System.Collections.Generic;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientList
{
    public class AddressToClientListVm
    {
        public IList<AddressToClientLookupDto> AddressToClients { get; set; }
    }
}
