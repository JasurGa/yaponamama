using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.FindStoreToGoodPagedList
{
    public class FindStoreToGoodPagedListQuery : IRequest<PageDto<StoreToGoodLookupDto>>
    {
        public Guid StoreId { get; set; }

        public string SearchQuery { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
