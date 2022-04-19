using System;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientDetails
{
    public class GetAddressToClientDetailsQuery : IRequest<AddressToClientDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
