using System;
using Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerPagedListNotByStoreId
{
    public class GetSupplyManagerPagedListNotByStoreIdQuery : IRequest<PageDto<SupplyManagerLookupDto>>
    {
        public Guid StoreId { get; set; }

        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
