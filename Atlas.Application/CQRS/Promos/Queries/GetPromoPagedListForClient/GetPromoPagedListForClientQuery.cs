using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoPagedListForClient
{
    public class GetPromoPagedListForClientQuery : IRequest<PageDto<PromoLookupDto>>
    {
        public Guid ClientId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }
    }
}
