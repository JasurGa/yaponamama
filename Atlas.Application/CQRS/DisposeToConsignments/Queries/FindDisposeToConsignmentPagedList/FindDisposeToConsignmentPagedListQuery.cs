using Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.FindDisposeToConsignmentPagedList
{
    public class FindDisposeToConsignmentPagedListQuery : IRequest<PageDto<DisposeToConsignmentLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
