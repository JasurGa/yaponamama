using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentDetails
{
    public class GetConsignmentDetailsQuery : IRequest<ConsignmentDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
