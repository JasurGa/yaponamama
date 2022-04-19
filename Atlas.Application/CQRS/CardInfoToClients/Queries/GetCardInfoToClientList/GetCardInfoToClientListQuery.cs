using System;
using MediatR;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientList
{
    public class GetCardInfoToClientListQuery : IRequest<CardInfoToClientListVm>
    {
        public Guid ClientId { get; set; }
    }
}
