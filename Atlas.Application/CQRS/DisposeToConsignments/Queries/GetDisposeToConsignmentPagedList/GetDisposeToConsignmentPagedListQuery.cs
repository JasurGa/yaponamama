using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList
{
    public class GetDisposeToConsignmentPagedListQuery : IRequest<PageDto<DisposeToConsignmentLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
