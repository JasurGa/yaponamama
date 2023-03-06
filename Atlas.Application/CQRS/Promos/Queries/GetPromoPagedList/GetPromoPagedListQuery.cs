using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoPagedList
{
    public class GetPromoPagedListQuery : IRequest<PageDto<PromoLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Sortable { get; set; }
        
        public bool Ascending { get; set; }

        public DateTime? FilterFromExpiresAt { get; set; }

        public DateTime? FilterToExpiresAt { get; set; }
    }
}
