using System;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Commands.DeleteStore
{
    public class DeleteStoreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
