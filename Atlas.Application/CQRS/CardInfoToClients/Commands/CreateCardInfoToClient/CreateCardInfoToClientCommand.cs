using System;
using MediatR;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient
{
    public class CreateCardInfoToClientCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string Token { get; set; }
    }
}
