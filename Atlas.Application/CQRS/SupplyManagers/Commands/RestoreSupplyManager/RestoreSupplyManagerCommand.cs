using MediatR;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.RestoreSupplyManager
{
    public class RestoreSupplyManagerCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
