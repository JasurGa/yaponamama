using System;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientList
{
    public class GetAddressToClientListQuery : IRequest<AddressToClientListVm>
    {
        public Guid ClientId { get; set; }
    }
}
