using Atlas.Application.CQRS.Stores.Queries.GetStoreList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Queries.GetStorePagedList
{
    public class GetStorePagedListQuery : IRequest<PageDto<StoreLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
