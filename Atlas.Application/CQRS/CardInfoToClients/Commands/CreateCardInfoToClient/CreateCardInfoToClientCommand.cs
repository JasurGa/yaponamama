using System;
using MediatR;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient
{
    public class CreateCardInfoToClientCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string CardNumber { get; set; }

        public string DateOfIssue { get; set; }

        public string Cvc { get; set; }

        public string Cvc2 { get; set; }

        public string CardHolder { get; set; }
    }
}
