using MediatR;
using System;

namespace Atlas.Application.CQRS.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
