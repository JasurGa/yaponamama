using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList
{
    public class GetSupplyManagerPagedListQuery : IRequest<PageDto<SupplyManagerLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
