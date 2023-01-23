using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.RestoreConsignment
{
    public class RestoreConsignmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
