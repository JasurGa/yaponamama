using System;
using MediatR;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientDetails
{
    public class GetClientDetailsQuery : IRequest<ClientDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
