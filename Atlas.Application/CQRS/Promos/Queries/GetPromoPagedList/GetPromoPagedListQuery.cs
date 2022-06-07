using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoPagedList
{
    public class GetPromoPagedListQuery : IRequest<PageDto<PromoLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
