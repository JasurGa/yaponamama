using System;
using MediatR;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientDetails
{
    public class GetCardInfoToClientDetailsQuery : IRequest<CardInfoToClientDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
