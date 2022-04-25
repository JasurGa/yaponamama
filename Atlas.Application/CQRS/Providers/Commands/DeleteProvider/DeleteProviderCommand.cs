using System;
using MediatR;

namespace Atlas.Application.CQRS.Providers.Commands.DeleteProvider
{
    public class DeleteProviderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
