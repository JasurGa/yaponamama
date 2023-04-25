using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByProvider
{
    public class GetGoodPagedListByProviderQuery : IRequest<PageDto<GoodLookupDto>>
    {
        public Guid ProviderId { get; set; }

        public bool ShowDeleted { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
