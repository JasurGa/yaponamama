using System;
using MediatR;

namespace Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage
{
    public class CreateChatMessageCommand : IRequest<Guid>
    {
        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public int MessageType { get; set; }

        public string Body { get; set; }

        public string Optional { get; set; }
    }
}
