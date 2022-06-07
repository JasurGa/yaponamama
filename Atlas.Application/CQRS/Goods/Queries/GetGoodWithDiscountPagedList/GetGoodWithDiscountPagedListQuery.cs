using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodWithDiscountPagedList
{
    public class GetGoodWithDiscountPagedListQuery : IRequest<PageDto<GoodLookupDto>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
