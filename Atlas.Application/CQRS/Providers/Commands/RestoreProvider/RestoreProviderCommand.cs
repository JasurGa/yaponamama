using System;
using MediatR;

namespace Atlas.Application.CQRS.Providers.Commands.RestoreProvider
{
    public class RestoreProviderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
