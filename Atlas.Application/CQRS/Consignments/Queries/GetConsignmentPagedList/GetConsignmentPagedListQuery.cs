using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentPagedList
{
    public class GetConsignmentPagedListQuery : IRequest<PageDto<ConsignmentLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
