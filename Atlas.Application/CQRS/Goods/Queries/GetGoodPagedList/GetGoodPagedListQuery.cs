using System;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedList
{
    public class GetGoodPagedListQuery : IRequest<PageDto<GoodLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Sortable { get; set; }

        public bool ShowDeleted { get; set; }

        public bool Ascending { get; set; }
    }
}
