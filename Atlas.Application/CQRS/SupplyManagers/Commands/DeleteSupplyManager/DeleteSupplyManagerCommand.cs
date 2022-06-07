using MediatR;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.DeleteSupplyManager
{
    public class DeleteSupplyManagerCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
