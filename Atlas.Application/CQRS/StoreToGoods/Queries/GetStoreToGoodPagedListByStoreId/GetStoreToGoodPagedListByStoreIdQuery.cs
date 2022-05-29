using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId
{
    public class GetStoreToGoodPagedListByStoreIdQuery : IRequest<PageDto<StoreToGoodLookupDto>>
    {
        public Guid StoreId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
