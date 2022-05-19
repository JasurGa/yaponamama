using System;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Commands.RestoreStore
{
    public class RestoreStoreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
