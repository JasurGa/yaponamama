using MediatR;
using System;

namespace Atlas.Application.CQRS.Clients.Commands.RestoreClient
{
    public class RestoreClientCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
