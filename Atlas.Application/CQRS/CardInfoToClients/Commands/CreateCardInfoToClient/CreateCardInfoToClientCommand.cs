using System;
using MediatR;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient
{
    public class CreateCardInfoToClientCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Expire { get; set; }

        public string Token { get; set; }

        public bool Recurrent { get; set; }

        public bool Verify { get; set; }
    }
}
