using System;
using MediatR;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.DeleteCardInfoToClient
{
    public class DeleteCardInfoToClientCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
