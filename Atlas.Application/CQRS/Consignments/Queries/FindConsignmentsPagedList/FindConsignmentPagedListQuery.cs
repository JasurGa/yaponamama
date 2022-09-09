using System;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList
{
    public class FindConsignmentPagedListQuery : IRequest<PageDto<ConsignmentLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

