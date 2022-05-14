using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class GetConsignmentListQuery : IRequest<ConsignmentListVm>
    {
        public Guid Id { get; set; }
    }
}
