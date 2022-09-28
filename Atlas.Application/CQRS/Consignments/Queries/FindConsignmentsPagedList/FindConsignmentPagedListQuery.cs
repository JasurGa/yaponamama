using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList
{
    public class FindConsignmentPagedListQuery : IRequest<PageDto<ConsignmentLookupDto>>
    {
        public string SearchQuery { get; set; }

        public DateTime? FilterStartDate { get; set; }

        public DateTime? FilterEndDate { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

