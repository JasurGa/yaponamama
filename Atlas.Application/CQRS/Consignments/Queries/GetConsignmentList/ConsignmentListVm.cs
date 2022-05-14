using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class ConsignmentListVm
    {
        public IList<ConsignmentLookupDto> Consignments { get; set; }
    }
}
