using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.DeleteConsignment
{
    public class DeleteConsignmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
