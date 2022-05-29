using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList
{
    public class GetSupportPagedListQuery : IRequest<PageDto<SupportLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
