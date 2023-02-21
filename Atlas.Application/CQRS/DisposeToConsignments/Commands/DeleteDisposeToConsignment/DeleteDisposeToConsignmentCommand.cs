using MediatR;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Commands.DeleteDisposeToConsignment
{
    public class DeleteDisposeToConsignmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
