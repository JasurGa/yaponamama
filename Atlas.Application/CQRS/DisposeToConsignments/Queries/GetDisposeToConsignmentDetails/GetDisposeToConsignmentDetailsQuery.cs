using MediatR;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentDetails
{
    public class GetDisposeToConsignmentDetailsQuery : IRequest<DisposeToConsignmentDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
