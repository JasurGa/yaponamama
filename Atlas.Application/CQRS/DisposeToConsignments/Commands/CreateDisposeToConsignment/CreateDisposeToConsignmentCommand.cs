using MediatR;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Commands.CreateDisposeToConsignment
{
    public class CreateDisposeToConsignmentCommand : IRequest<Guid>
    {
        public Guid ConsignmentId { get; set; }

        public int Count { get; set; }

        public string Comment { get; set; }
    }
}
